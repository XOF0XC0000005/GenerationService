﻿using GenerationService.Models;
using System.Text.Json.Serialization;

namespace GenerationService.Persisntence
{
    [JsonSerializable(typeof(ExcelDataRequest))]
    [JsonSerializable(typeof(List<FileMeta>))]
    [JsonSerializable(typeof(FileResult))]
    public partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
