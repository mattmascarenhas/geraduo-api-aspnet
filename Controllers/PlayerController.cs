using Dapper;
using geraduo.Authenticate;
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
        [HttpPost("v1/players")]
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
        public object DeletePlayer(Guid id) {
            try {
                // exclui o jogo do banco de dados usando a stored procedure
                _context.Connection.Execute("spDeletePlayer", new {
                    Id = id
                }, commandType: System.Data.CommandType.StoredProcedure);

                return "Player deleted successfully!";
            } catch {
                return BadRequest("There is an Ad using this Player! ");

            }

        }
        //autenticar
        [HttpPost("v1/authentication")]
        public IActionResult Authenticate([FromBody] AuthenticationRequest request) {
            // Verificar se o email e a senha são válidos
            if (!IsValidCredentials(request.Email, request.Password))
                return Unauthorized("Credenciais inválidas");

            // Lógica para gerar token de autenticação
            var token = GenerateToken(request.Email);

            var player = _context.Connection.QueryFirstOrDefault<Player>("SELECT [Id], [Name], [Nickname], [Email], [Password], [Discord] FROM [Players] WHERE [Email] = @Email",
        new {
            Email = request.Email
        });

            // Retornar o token para o cliente
            return Ok(new {
                Id = player.Id,
                Name = player.Name,
                NickName = player.Nickname,
                Email = request.Email,
                Discord = player.Discord,
                Token = token
            });
        }

        private bool IsValidCredentials(string email, string password) {
            var player = _context.Connection.QueryFirstOrDefault<Player>("SELECT [Id], [Name], [Nickname], [Email], [Password], [Discord] FROM [Players] WHERE [Email] = @Email",
                new {
                    Email = email
                });

            if (player == null)
                return false;

            // Lógica para comparar senhas
            if (player.Password != password)
                return false;

            return true;
        }

        private string GenerateToken(string email) {
            var token = email + System.DateTime.UtcNow.ToString();
            return token;
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