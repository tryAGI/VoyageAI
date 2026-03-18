namespace VoyageAI;

/// <summary>
/// A delegating handler that retries requests when receiving HTTP 429 (Too Many Requests)
/// responses, using exponential backoff. Respects the Retry-After header if present.
/// </summary>
public class RateLimitRetryHandler : DelegatingHandler
{
    private readonly int _maxRetries;
    private readonly TimeSpan _initialDelay;

    /// <summary>
    /// Creates a new instance of <see cref="RateLimitRetryHandler"/>.
    /// </summary>
    /// <param name="maxRetries">Maximum number of retries. Default is 3.</param>
    /// <param name="initialDelay">Initial delay before the first retry. Default is 1 second. Subsequent retries double the delay.</param>
    #pragma warning disable CA2000 // DelegatingHandler takes ownership of the inner handler
    public RateLimitRetryHandler(
        int maxRetries = 3,
        TimeSpan? initialDelay = null)
        : base(new HttpClientHandler())
    #pragma warning restore CA2000
    {
        _maxRetries = maxRetries;
        _initialDelay = initialDelay ?? TimeSpan.FromSeconds(1);
    }

    /// <summary>
    /// Creates a new instance of <see cref="RateLimitRetryHandler"/> with a specific inner handler.
    /// </summary>
    /// <param name="innerHandler">The inner handler to delegate to.</param>
    /// <param name="maxRetries">Maximum number of retries. Default is 3.</param>
    /// <param name="initialDelay">Initial delay before the first retry. Default is 1 second. Subsequent retries double the delay.</param>
    public RateLimitRetryHandler(
        HttpMessageHandler innerHandler,
        int maxRetries = 3,
        TimeSpan? initialDelay = null)
        : base(innerHandler)
    {
        _maxRetries = maxRetries;
        _initialDelay = initialDelay ?? TimeSpan.FromSeconds(1);
    }

    /// <inheritdoc/>
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response = null!;

        for (var attempt = 0; attempt <= _maxRetries; attempt++)
        {
            response = await base.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            if ((int)response.StatusCode != 429 || attempt == _maxRetries)
            {
                return response;
            }

            var delay = GetDelay(response, attempt);

            response.Dispose();

            await Task.Delay(delay, cancellationToken)
                .ConfigureAwait(false);
        }

        return response;
    }

    private TimeSpan GetDelay(HttpResponseMessage response, int attempt)
    {
        // Respect Retry-After header if present
        if (response.Headers.RetryAfter is { } retryAfter)
        {
            if (retryAfter.Delta is { } delta)
            {
                return delta;
            }

            if (retryAfter.Date is { } date)
            {
                var delay = date - DateTimeOffset.UtcNow;
                if (delay > TimeSpan.Zero)
                {
                    return delay;
                }
            }
        }

        // Exponential backoff: 1s, 2s, 4s, ...
        return TimeSpan.FromTicks(_initialDelay.Ticks * (1L << attempt));
    }
}
