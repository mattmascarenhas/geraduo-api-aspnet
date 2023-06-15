using Dapper;
using geraduo.Entities;
using geraduo.QueriesResult;
using Microsoft.AspNetCore.Mvc;

namespace geraduo.Controllers {
    public class AdController : Controller {
        private readonly Database _context;
        public AdController(Database context) {
            _context = context;
        }

        //listar todos
        [HttpGet("v1/ads")]
        public IEnumerable<AdQueryResult> GetAllAds() {
            return _context
                    .Connection
                    .Query<AdQueryResult>("SELECT [Id], [PlayerId], [GameId], [PlayerName], [WeekDays], [HourStart], [HourEnd] FROM [Ads]");
        }
        //listar um
        [HttpGet("v1/ad/{id}")]
        public AdQueryResult GetAd(Guid id) {
            return _context
                    .Connection
                    .Query<AdQueryResult>("SELECT [Id], [PlayerId], [GameId], [PlayerName], [WeekDays], [HourStart], [HourEnd] FROM [Ads] " +
                    "WHERE [Id]=@id",
                    new {
                        id = id,
                    }).FirstOrDefault();
        }
        //criar
        [HttpPost("v1/ad")]
        public object PostAd([FromBody] Ad ad) {
            //instancia o ad de acordo com o que vem no body
            var _ad = new Ad(ad.PlayerId, ad.GameId, ad.PlayerName, ad.WeekDays, ad.HourStart, ad.HourEnd);

            //salvar o ad no banco de dados
            _context.Connection.Execute("spCreateAd", new {
                Id = _ad.Id,
                PlayerId = _ad.PlayerId,
                GameId = _ad.GameId,
                PlayerName = _ad.PlayerName,
                WeekDays = _ad.WeekDays,
                HourStart = _ad.HourStart,
                HourEnd = _ad.HourEnd,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            }, commandType: System.Data.CommandType.StoredProcedure);

            return _ad;
        }
        //editar
        [HttpPut("v1/ad/{id}")]
        public string PutAd(Guid id, [FromBody] Ad ad) {
            // atualiza o ad de acordo com o que vem no body
            var _updatedAd = new Ad(ad.PlayerId, ad.GameId, ad.PlayerName, ad.WeekDays, ad.HourStart, ad.HourEnd);

            // atualiza o ad no banco de dados usando a stored procedure
            _context.Connection.Execute("spUpdateAd", new {
                Id = id,
                PlayerId = _updatedAd.PlayerId,
                GameId = _updatedAd.GameId,
                PlayerName = _updatedAd.PlayerName,
                WeekDays = _updatedAd.WeekDays,
                HourStart = _updatedAd.HourStart,
                HourEnd = _updatedAd.HourEnd,
            }, commandType: System.Data.CommandType.StoredProcedure);

            return "Ad updated successfully!";
        }

        [HttpDelete("v1/ad/{id}")]
        public object DeleteAd(Guid id) {
            try {
                // exclui o ad do banco de dados usando a stored procedure
                _context.Connection.Execute("spDeleteAd", new {
                    Id = id
                }, commandType: System.Data.CommandType.StoredProcedure);

                return "Ad deleted successfully!";
            } catch (IOException ex){
                return BadRequest($"Error: {ex.Message}");

            }

        }

    }
}
