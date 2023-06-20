# GeraDuo - API

Nessa API, temos três entidades principais que são, PLAYER, GAME e AD.
PLAYER representa o jogador que irá se cadastrar no sistema para buscar outro jogador compatível.
GAME representa os jogos disponíveis na plataforma para fazer o pareamento.
AD é o anúncio que os PLAYERS criam contendo informações, incluindo o GAME, para que haja o pareamento de PLAYERS.

O micro ORM utilizado é o Dapper.
A forma de buscar os dados e executar operações é através de procedures e views.

A API também faz a geração do token de maneira segura e eficiente, para quando houver uma autenticação do usuário no front-end.
