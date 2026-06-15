#nullable enable
#pragma warning disable CS0618

using System.CommandLine;

namespace VoyageAI.CLI.Commands;

internal static partial class EndpointsRerankerApiCommandApiCommand
{
    private static Option<string> Query { get; } = new(
        name: @"--query")
    {
        Description = @"The query as a string. The query can contain a maximum of 4000 tokens for `rerank-2`, 2000 tokens for `rerank-2-lite` and `rerank-1`, and 1000 tokens for `rerank-lite-1`.
",
        Required = true,
    };

    private static Option<global::System.Collections.Generic.IList<string>> Documents { get; } = new(
        name: @"--documents")
    {
        Description = @"The documents to be reranked as a list of strings. <ul> <li> The number of documents cannot exceed 1000. </li> <li> The sum of the number of tokens in the query and the number of tokens in any single document cannot exceed 16000 for `rerank-2`; 8000 for `rerank-2-lite` and `rerank-1`; and 4000 for `rerank-lite-1`. </li> <li> The total number of tokens, defined as ""the number of query tokens × the number of documents + sum of the number of tokens in all documents"", cannot exceed 600K for `rerank-2` and `rerank-2-lite`, and 300K for `rerank-1` and `rerank-lite-1`. Please see our <a href=""https://docs.voyageai.com/docs/faq#what-is-the-total-number-of-tokens-for-the-rerankers"">FAQ</a>. </li> </ul>
",
        Required = true,
    };

    private static Option<string> Model { get; } = new(
        name: @"--model")
    {
        Description = @"Name of the model. Recommended options: `rerank-2`, `rerank-2-lite`.
",
        Required = true,
    };

    private static Option<int?> TopK { get; } = new(
        name: @"--top-k")
    {
        Description = @"The number of most relevant documents to return. If not specified, the reranking results of all documents will be returned.
",
    };

    private static Option<bool?> ReturnDocuments { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--return-documents",
        description: @"Whether to return the documents in the response. Defaults to `false`. <ul> <li> If `false`, the API will return a list of {""index"", ""relevance_score""} where ""index"" refers to the index of a document within the input list. </li> <li> If `true`, the API will return a list of {""index"", ""document"", ""relevance_score""} where ""document"" is the corresponding document from the input list. </li> </ul>
");

    private static Option<bool?> Truncation { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--truncation",
        description: @"Whether to truncate the input to satisfy the ""context length limit"" on the query and the documents. Defaults to `true`. <ul> <li> If `true`,  the query and documents will be truncated to fit within the context length limit, before processed by the reranker model. </li> <li> If `false`, an error will be raised when the query exceeds 4000 tokens for `rerank-2`; 2000 tokens `rerank-2-lite` and `rerank-1`; and 1000 tokens for `rerank-lite-1`, or the sum of the number of tokens in the query and the number of tokens in any single document exceeds 16000 for `rerank-2`; 8000 for `rerank-2-lite` and `rerank-1`; and 4000 for `rerank-lite-1`. </li> </ul>
");
      private static Option<string?> Input { get; } = new(@"--input")
      {
          Description = "Load request JSON from a file path, '-' for stdin, or an inline JSON object/array string.",
      };

      private static Option<string?> RequestJson { get; } = new(@"--request-json")
      {
          Description = "Request body as JSON.",
          Hidden = true,
      };

      private static Option<string?> RequestFile { get; } = new(@"--request-file")
      {
          Description = "Path to a JSON request file, or '-' for stdin.",
          Hidden = true,
      };

                    private static string FormatResponse(ParseResult parseResult, global::VoyageAI.RerankerApiResponse value, global::System.Text.Json.Serialization.JsonSerializerContext context, bool truncateLongStrings)
                    {
                        string? text = null;
                        CustomizeResponseText(parseResult, value, ref text);
                        if (!string.IsNullOrWhiteSpace(text))
                        {
                            return text;
                        }

                        var hints = new Dictionary<string, CliFormatHint>(StringComparer.OrdinalIgnoreCase)
                        {
                        };
                        CustomizeResponseFormatHints(hints);
                        return CliRuntime.FormatHumanReadable(value, context, truncateLongStrings, hints);
                    }

                    static partial void CustomizeResponseText(ParseResult parseResult, global::VoyageAI.RerankerApiResponse value, ref string? text);
                    static partial void CustomizeResponseFormatHints(Dictionary<string, CliFormatHint> hints);


    public static Command Create()
    {
        var command = new Command(@"reranker-api", @"Rerankers
Voyage reranker endpoint receives as input a query, a list of documents, and other arguments such as the model name, and returns a response containing the reranking results.
");
                        command.Options.Add(Query);
                        command.Options.Add(Documents);
                        command.Options.Add(Model);
                        command.Options.Add(TopK);
                        command.Options.Add(ReturnDocuments);
                        command.Options.Add(Truncation);
          command.Options.Add(Input);
          command.Options.Add(RequestJson);
          command.Options.Add(RequestFile);
          command.Validators.Add(result =>
          {
              var hasInput = result.GetResult(Input) is not null;
              var hasRequestJson = result.GetResult(RequestJson) is not null;
              var hasRequestFile = result.GetResult(RequestFile) is not null;
              var specifiedCount = (hasInput ? 1 : 0) + (hasRequestJson ? 1 : 0) + (hasRequestFile ? 1 : 0);
              if (specifiedCount > 1)
              {
                  result.AddError(@"Specify at most one of --input, --request-json, or --request-file.");
              }
          });

        command.SetAction(async (ParseResult parseResult, CancellationToken cancellationToken) =>
            await CliRuntime.RunAsync(async () =>
            {
                        var __requestBase = await CliRuntime.ReadRequestOrDefaultAsync<global::VoyageAI.RerankerApiRequest>(
                            parseResult,
                            Input,
                            RequestJson,
                            RequestFile,
                            global::VoyageAI.SourceGenerationContext.Default,
                            cancellationToken).ConfigureAwait(false);
                        var query = parseResult.GetRequiredValue(Query);
                        var documents = parseResult.GetRequiredValue(Documents);
                        var model = parseResult.GetRequiredValue(Model);
                        var topK = CliRuntime.WasSpecified(parseResult, TopK) ? parseResult.GetValue(TopK) : (__requestBase is { } __TopKBaseValue ? __TopKBaseValue.TopK : default);
                        var returnDocuments = CliRuntime.WasSpecified(parseResult, ReturnDocuments) ? parseResult.GetValue(ReturnDocuments) : (__requestBase is { } __ReturnDocumentsBaseValue ? __ReturnDocumentsBaseValue.ReturnDocuments : default);
                        var truncation = CliRuntime.WasSpecified(parseResult, Truncation) ? parseResult.GetValue(Truncation) : (__requestBase is { } __TruncationBaseValue ? __TruncationBaseValue.Truncation : default);
                using var client = await CliRuntime.CreateClientAsync(parseResult, cancellationToken).ConfigureAwait(false);


                                var response = await client.RerankerApiAsync(
                                    query: query,
                                    documents: documents,
                                    model: model,
                                    topK: topK,
                                    returnDocuments: returnDocuments,
                                    truncation: truncation,
                                    cancellationToken: cancellationToken).ConfigureAwait(false);


                                if (!await CliRuntime.TryWriteOutputDirectoryAsync(
                                        parseResult,
                                        response,
                                        global::VoyageAI.SourceGenerationContext.Default,
                                        @"Data",
                                        cancellationToken).ConfigureAwait(false))
                                {
                                await CliRuntime.WriteResponseAsync(
                                    parseResult,
                                    response,
                                    global::VoyageAI.SourceGenerationContext.Default,
                                    FormatResponse,
                                    cancellationToken).ConfigureAwait(false);
                                }
            }, cancellationToken).ConfigureAwait(false));
        return command;
    }
}