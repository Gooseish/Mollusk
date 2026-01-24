using System;
using System.Collections.Generic;
using JsonPipeline;
using MolluskEngine.GameBoard;

namespace MolluskEngine.Data;

internal class TerrainDataReader : JsonContentTypeReader<Dictionary<int, Terrain>>
{

}
