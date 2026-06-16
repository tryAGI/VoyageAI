#nullable enable
#pragma warning disable CS0618

using System.CommandLine;

namespace VoyageAI.CLI.Commands;

internal static partial class EndpointsEmbeddingsApiCommandApiCommand
{
    private static Option<global::VoyageAI.OneOf<string, global::System.Collections.Generic.IList<string>>> InputOption { get; } = new(
        name: @"--input")
    {
        Description = @"A single text string, or a list of texts as a list of strings, such as `[""I like cats"", ""I also like dogs""]`. Currently, we have two constraints on the list: <ul>  <li> The maximum length of the list is 128. </li>  <li> The total number of tokens in the list is at most 1M for `voyage-3-lite`; 320K for `voyage-3` and `voyage-2`; and 120K for `voyage-3-large`, `voyage-code-3`, `voyage-large-2-instruct`, `voyage-finance-2`, `voyage-multilingual-2`, `voyage-law-2`, and `voyage-large-2`. </li> <ul>
",
        Required = true,
    };

    private static Option<string> Model { get; } = new(
        name: @"--model")
    {
        Description = @"Name of the model. Recommended options: `voyage-3-large`, `voyage-3`, `voyage-3-lite`, `voyage-code-3`, `voyage-finance-2`, `voyage-law-2`.
",
        Required = true,
    };

    private static Option<global::VoyageAI.EmbeddingsApiRequestInputType?> InputType { get; } = new(
        name: @"--input-type")
    {
        Description = @"Type of the input text. Defaults to `null`. Other options: `query`, `document`. <ul> <li> When `input_type` is `null`, the embedding model directly converts the inputs (`texts`) into numerical vectors. For retrieval/search purposes, where a ""query"" is used to search for relevant information among a collection of data referred to as ""documents,"" we recommend specifying whether your inputs (`texts`) are intended as queries or documents by setting `input_type` to `query` or `document`, respectively. In these cases, Voyage automatically prepends a prompt to your `inputs` before vectorizing them, creating vectors more tailored for retrieval/search tasks. Embeddings generated with and without the `input_type` argument are compatible. </li> <li> For transparency, the following prompts are prepended to your input. </li>
  <ul>
    <li> For <code>query</code>, the prompt is <i>""Represent the query for retrieving supporting documents: "".</i> </li>
    <li> For <code>document</code>, the prompt is <i>""Represent the document for retrieval: "".</i> </li>
  </ul>
<ul> <ul>
",
    };

    private static Option<bool?> Truncation { get; } = CliRuntime.CreateNullableBoolOption(
        name: @"--truncation",
        description: @"Whether to truncate the input texts to fit within the context length. Defaults to `true`. <ul>  <li> If `true`, an over-length input texts will be truncated to fit within the context length, before vectorized by the embedding model. </li>  <li> If `false`, an error will be raised if any given text exceeds the context length. </li>  </ul>
");

    private static Option<int?> OutputDimension { get; } = new(
        name: @"--output-dimension")
    {
        Description = @"The number of dimensions for resulting output embeddings. Defaults to `null`. <ul> <li> Most models only support a single default dimension, used when `output_dimension` is set to `null` (see output embedding dimensions <a href=""https://docs.voyageai.com/docs/embeddings"" target=""_blank"">here</a>). </li> <li> `voyage-3-large` and `voyage-code-3` support the following `output_dimension` values: 2048, 1024 (default), 512, and 256. </li> </ul>
",
    };

    private static Option<global::VoyageAI.EmbeddingsApiRequestOutputDtype?> OutputDtype { get; } = new(
        name: @"--output-dtype")
    {
        Description = @"The data type for the embeddings to be returned. Defaults to `float`. Other options: `int8`, `uint8`, `binary`, `ubinary`. `float` is supported for all models. `int8`, `uint8`, `binary`, and `ubinary` are supported by `voyage-3-large` and `voyage-code-3`. Please see our <a href=""https://docs.voyageai.com/docs/flexible-dimensions-and-quantization#quantization"" target=""_blank"">guide</a> for more details about output data types. <ul> <li> `float`: Each returned embedding is a list of 32-bit (4-byte) <a href=""https://en.wikipedia.org/wiki/Single-precision_floating-point_format"" target=""_blank"">single-precision floating-point</a> numbers. This is the default and provides the highest precision / retrieval accuracy. </li> <li> `int8` and `uint8`: Each returned embedding is a list of 8-bit (1-byte) integers ranging from -128 to 127 and 0 to 255, respectively. </li> <li> `binary` and `ubinary`: Each returned embedding is a list of 8-bit integers that represent bit-packed, quantized single-bit embedding values: `int8` for `binary` and `uint8` for `ubinary`. The length of the returned list of integers is 1/8 of `output_dimension` (which is the actual dimension of the embedding). The `binary` type uses the offset binary method. Please refer to our guide for details on <a href=""https://docs.voyageai.com/docs/flexible-dimensions-and-quantization#offset-binary"" target=""_blank"">offset binary</a> and <a href=""https://docs.voyageai.com/docs/flexible-dimensions-and-quantization#quantization"" target=""_blank"">binary embeddings</a>.  </ul>
",
    };

    private static Option<global::VoyageAI.EmbeddingsApiRequestEncodingFormat?> EncodingFormat { get; } = new(
        name: @"--encoding-format")
    {
        Description = @"Format in which the embeddings are encoded. Defaults to `null`. Other options: `base64`. <ul> <li> If `null`, each embedding is an array of float numbers when `output_dtype` is set to `float` and as an array of integers for all other values of `output_dtype` (`int8`, `uint8`, `binary`, and `ubinary`). <li> If `base64`, the embeddings are represented as a <a href=""https://docs.python.org/3/library/base64.html"" target=""_blank"">Base64-encoded</a> NumPy array of: </li>
  <ul>
    <li> Floating-point numbers (<a href=""https://numpy.org/doc/2.1/user/basics.types.html#numerical-data-types"" target=""_blank"">numpy.float32</a>) for <code>output_dtype</code> set to <code>float</code>. </li>
    <li> Signed integers (<a href=""https://numpy.org/doc/2.1/user/basics.types.html#numerical-data-types"" target=""_blank"">numpy.int8</a>) for <code>output_dtype</code> set to <code>int8</code> or <code>binary</code>. </li>
    <li> Unsigned integers (<a href=""https://numpy.org/doc/2.1/user/basics.types.html#numerical-data-types"" target=""_blank"">numpy.uint8</a>) for <code>output_dtype</code> set to <code>uint8</code> or <code>ubinary</code>. </li>
  </ul>
</ul>
",
    };
      private static Option<string?> RequestInput { get; } = new(@"--request-input")
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

                    private static string FormatResponse(ParseResult parseResult, global::VoyageAI.EmbeddingsApiResponse value, global::System.Text.Json.Serialization.JsonSerializerContext context, bool truncateLongStrings)
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

                    static partial void CustomizeResponseText(ParseResult parseResult, global::VoyageAI.EmbeddingsApiResponse value, ref string? text);
                    static partial void CustomizeResponseFormatHints(Dictionary<string, CliFormatHint> hints);


    public static Command Create()
    {
        var command = new Command(@"embeddings-api", @"Text embedding models
Voyage text embedding endpoint receives as input a string (or a list of strings) and other arguments such as the preferred model name, and returns a response containing a list of embeddings.");
                        command.Options.Add(InputOption);
                        command.Options.Add(Model);
                        command.Options.Add(InputType);
                        command.Options.Add(Truncation);
                        command.Options.Add(OutputDimension);
                        command.Options.Add(OutputDtype);
                        command.Options.Add(EncodingFormat);
          command.Options.Add(RequestInput);
          command.Options.Add(RequestJson);
          command.Options.Add(RequestFile);
          command.Validators.Add(result =>
          {
              var hasInput = result.GetResult(RequestInput) is not null;
              var hasRequestJson = result.GetResult(RequestJson) is not null;
              var hasRequestFile = result.GetResult(RequestFile) is not null;
              var specifiedCount = (hasInput ? 1 : 0) + (hasRequestJson ? 1 : 0) + (hasRequestFile ? 1 : 0);
              if (specifiedCount > 1)
              {
                  result.AddError(@"Specify at most one of --request-input, --request-json, or --request-file.");
              }
          });

        command.SetAction(async (ParseResult parseResult, CancellationToken cancellationToken) =>
            await CliRuntime.RunAsync(async () =>
            {
                        var __requestBase = await CliRuntime.ReadRequestOrDefaultAsync<global::VoyageAI.EmbeddingsApiRequest>(
                            parseResult,
                            RequestInput,
                            RequestJson,
                            RequestFile,
                            global::VoyageAI.SourceGenerationContext.Default,
                            cancellationToken).ConfigureAwait(false);
                        var input = parseResult.GetRequiredValue(InputOption);
                        var model = parseResult.GetRequiredValue(Model);
                        var inputType = CliRuntime.WasSpecified(parseResult, InputType) ? parseResult.GetValue(InputType) : (__requestBase is { } __InputTypeBaseValue ? __InputTypeBaseValue.InputType : default);
                        var truncation = CliRuntime.WasSpecified(parseResult, Truncation) ? parseResult.GetValue(Truncation) : (__requestBase is { } __TruncationBaseValue ? __TruncationBaseValue.Truncation : default);
                        var outputDimension = CliRuntime.WasSpecified(parseResult, OutputDimension) ? parseResult.GetValue(OutputDimension) : (__requestBase is { } __OutputDimensionBaseValue ? __OutputDimensionBaseValue.OutputDimension : default);
                        var outputDtype = CliRuntime.WasSpecified(parseResult, OutputDtype) ? parseResult.GetValue(OutputDtype) : (__requestBase is { } __OutputDtypeBaseValue ? __OutputDtypeBaseValue.OutputDtype : default);
                        var encodingFormat = CliRuntime.WasSpecified(parseResult, EncodingFormat) ? parseResult.GetValue(EncodingFormat) : (__requestBase is { } __EncodingFormatBaseValue ? __EncodingFormatBaseValue.EncodingFormat : default);
                using var client = await CliRuntime.CreateClientAsync(parseResult, cancellationToken).ConfigureAwait(false);


                                var response = await client.EmbeddingsApiAsync(
                                    input: input,
                                    model: model,
                                    inputType: inputType,
                                    truncation: truncation,
                                    outputDimension: outputDimension,
                                    outputDtype: outputDtype,
                                    encodingFormat: encodingFormat,
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