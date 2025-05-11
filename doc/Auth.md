# Auth

###### [Página inicial](../README.md)

### /api/auth

Esse endpoint é o responsável por autenticar o usuário e retornar o token JWT necessário para utilizar os outros endpoints do projeto.

- **POST** _/api/auth_ - Realiza a autenticação utilizando o email e senha do usuário

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "email": "string", //email do usuário
    "password": "string" //senha do usuário
  }
  ```

  - Retorno (application/json)

  ```jsonc
  {
    "success": true, //indica se a requisição teve sucesso ou não
    "message": "string", //mensagem retornada pela api
    "errors": [
      //lista de erros caso ocorra
      {
        "error": "string", //tipo do erro
        "detail": "string" //mensagem do erro
      }
    ],
    "data": {
      //dados de autenticação caso tenha sucesso
      "token": "string", //token JWT
      "email": "string", //email do usuário
      "name": "string", //nome do usuário
      "role": "string" //role a qual o usuário está vinculado
    }
  }
  ```
