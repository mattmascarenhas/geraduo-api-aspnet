using Dapper;
using geraduo.Entities;
using geraduo.QueriesResult;
using Microsoft.AspNetCore.Mvc;

namespace geraduo.Controllers {
    public class PlayerController: Controller {
        private readonly Database _context;
        public PlayerController(Database context) {
            _context = context;
        }

        //listar todos
        [HttpGet("v1/players")]
        public IEnumerable<PlayerQueryResult> GetAllPlayers() {
            return _context
                    .Connection
                    .Query<PlayerQueryResult>("SELECT [Id], [Name], [Nickname], [Email], [Discord] FROM [Players]");
        }
        //listar um
        [HttpGet("v1/player/{id}")]
        public PlayerQueryResult GetPlayer(Guid id) {
            return _context
                    .Connection
                    .Query<PlayerQueryResult>("SELECT [Id], [Name], [Nickname], [Email], [Discord] FROM [Players] WHERE [Id]=@id",
                    new {
                        id = id,
                    }).FirstOrDefault();
        }
        //criar
        [HttpPost("v1/player")]
        public object PostPlayer([FromBody] Player player) {
            //verifica se o email ja existe
            if (CheckEmail(player.Email))
                return BadRequest("O Email já está cadastrado");
            //instancia o player de acordo com o que vem no body
            var _player = new Player(player.Name, player.Nickname, player.Email, player.Password, player.Discord);

            //salvar o player no banco de dados
            _context.Connection.Execute("spCreatePlayer", new {
                Id = _player.Id,
                Name = _player.Name,
                Nickname = _player.Nickname,
                Email = _player.Email,
                Password = _player.Password,
                Discord = _player.Discord,
                CreatedAt = DateTime.Now
            }, commandType: System.Data.CommandType.StoredProcedure);

            return _player;
        }
        //editar
        [HttpPut("v1/player/{id}")]
        public object PutPlayer(Guid id, [FromBody] Player player) {
            //verifica se o email ja existe
            if (CheckEmail(player.Email))
                return BadRequest("O Email já está cadastrado");
            // atualiza o player de acordo com o que vem no body
            var _updatedPlayer = new Player(player.Name, player.Nickname, player.Email, player.Password, player.Discord);

            // atualiza o player no banco de dados usando a stored procedure
            _context.Connection.Execute("spUpdatePlayer", new {
                Id = id,
                Name = _updatedPlayer.Name,
                Nickname = _updatedPlayer.Nickname,
                Email = _updatedPlayer.Email,
                Password = _updatedPlayer.Password,
                Discord = _updatedPlayer.Discord,
            }, commandType: System.Data.CommandType.StoredProcedure);

            return "Player updated successfully!";
        }
        //deletar
        [HttpDelete("v1/player/{id}")]
        public string DeletePlayer(Guid id) {
            // exclui o jogo do banco de dados usando a stored procedure
            _context.Connection.Execute("spDeletePlayer", new {
                Id = id
            }, commandType: System.Data.CommandType.StoredProcedure);

            return "Player deleted successfully!";
        }

        //email será checado através de uma procedure
        public bool CheckEmail(string email) {
            return _context
                     .Connection
                     .Query<bool>("spCheckEmail", new {
                         Email = email
                     }, commandType: System.Data.CommandType.StoredProcedure)
                     .FirstOrDefault();
        }
    }
}
