using Dapper;
using geraduo.Entities;
using geraduo.QueriesResult;
using Microsoft.AspNetCore.Mvc;

namespace geraduo.Controllers {
    public class GamesController: Controller {
        private readonly Database _context;
        public GamesController(Database context) {
            _context = context;
        }

        //listar todos
        [HttpGet("v1/games")]
        public IEnumerable<GameQueryResult> GetAllGames() {
            return _context
                    .Connection
                    .Query<GameQueryResult>("SELECT [Id], [Title], [BannerUrl] FROM [Games]", new { });
        }
        //listar um
        [HttpGet("v1/games/{id}")]
        public GameQueryResult GetGame(Guid id) {
            return _context
                    .Connection
                    .Query<GameQueryResult>("SELECT [Id], [Title], [BannerUrl] FROM [Games] WHERE [Id]=@id",
                    new {
                        id = id
                    }).FirstOrDefault();
        }
        //criar
        [HttpPost("v1/game")]
        public Game PostGame([FromBody] Game game) {
            //instancia o game de acordo com o que vem no body
            var _game = new Game(game.Title, game.BannerUrl);

            //salvar o game no banco de dados
            _context.Connection.Execute("spCreateGame", new {
                Id = _game.Id,
                Title = _game.Title,
                BannerUrl = _game.BannerUrl
            }, commandType: System.Data.CommandType.StoredProcedure);

            return _game;
        }
        //editar
        [HttpPut("v1/game/{id}")]
        public object PutGame(Guid id, [FromBody] Game game) {
            try {
                // atualiza o game de acordo com o que vem no body
                var updatedGame = new Game(game.Title, game.BannerUrl);

                // atualiza o jogo no banco de dados usando a stored procedure
                _context.Connection.Execute("spUpdateGame", new {
                    Id = id,
                    Title = updatedGame.Title,
                    BannerUrl = updatedGame.BannerUrl
                }, commandType: System.Data.CommandType.StoredProcedure);

                return "Game updated successfully!";
            } catch (IOException ex) {
                return BadRequest($"Error: {ex.Message}");
            }

        }
        //deletar
        [HttpDelete("v1/game/{id}")]
        public object DeleteGame(Guid id) {
            try {
                // exclui o jogo do banco de dados usando a stored procedure
                _context.Connection.Execute("spDeleteGame", new {
                    Id = id
                }, commandType: System.Data.CommandType.StoredProcedure);

                return "Game deleted successfully!";
            } catch {
                return BadRequest("There is an Ad using this Game! ");
            }
        }
    }
}
