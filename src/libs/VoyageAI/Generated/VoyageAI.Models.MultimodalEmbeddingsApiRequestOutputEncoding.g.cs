
#nullable enable

namespace VoyageAI
{
    /// <summary>
    /// Format in which the embeddings are encoded. Defaults to `null`. &lt;ul&gt; &lt;li&gt; If `null`, the embeddings are represented as a list of floating-point numbers. &lt;/li&gt;  &lt;li&gt; If `base64`, the embeddings are represented as a Base64-encoded NumPy array of single-precision floats. &lt;/li&gt;  &lt;/ul&gt;<br/>
    /// Default Value: openapi-json-null-sentinel-value-2BF93600-0FE4-4250-987A-E5DDB203E464
    /// </summary>
    public enum MultimodalEmbeddingsApiRequestOutputEncoding
    {
        /// <summary>
        /// 
        /// </summary>
        OpenapiJsonNullSentinelValue2bf936000fe44250987aE5ddb203e464,
        /// <summary>
        /// 
        /// </summary>
        Base64,
    }

    /// <summary>
    /// Enum extensions to do fast conversions without the reflection.
    /// </summary>
    public static class MultimodalEmbeddingsApiRequestOutputEncodingExtensions
    {
        /// <summary>
        /// Converts an enum to a string.
        /// </summary>
        public static string ToValueString(this MultimodalEmbeddingsApiRequestOutputEncoding value)
        {
            return value switch
            {
                MultimodalEmbeddingsApiRequestOutputEncoding.OpenapiJsonNullSentinelValue2bf936000fe44250987aE5ddb203e464 => "openapi-json-null-sentinel-value-2BF93600-0FE4-4250-987A-E5DDB203E464",
                MultimodalEmbeddingsApiRequestOutputEncoding.Base64 => "base64",
                _ => throw new global::System.ArgumentOutOfRangeException(nameof(value), value, null),
            };
        }
        /// <summary>
        /// Converts an string to a enum.
        /// </summary>
        public static MultimodalEmbeddingsApiRequestOutputEncoding? ToEnum(string value)
        {
            return value switch
            {
                "openapi-json-null-sentinel-value-2BF93600-0FE4-4250-987A-E5DDB203E464" => MultimodalEmbeddingsApiRequestOutputEncoding.OpenapiJsonNullSentinelValue2bf936000fe44250987aE5ddb203e464,
                "base64" => MultimodalEmbeddingsApiRequestOutputEncoding.Base64,
                _ => null,
            };
        }
    }
}