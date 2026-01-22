using System;
using System.Collections.Generic;
using JsonContentPipeline;
using MolluskEngine.GameBoard;

namespace MolluskEngine.Data;

internal class TerrainDataReader : JsonContentTypeReader<Dictionary<int, Terrain>>
{

}
