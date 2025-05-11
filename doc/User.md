# User

###### [Página inicial](../README.md)

### /api/users

Esse endpoint é o responsável por consultar e gerenciar os usuários cadastrados. Com exceção do endpoint do comando POST, o usuário precisar estar autenticado para poder executar as operações. Caso o usuário tenha uma função (role) de _customer_, ele terá acesso somente aos seus dados.

- **POST** _/api/users_ - Cria um novo usuário

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "email": "string", //Email do usuário. Deve ser único
    "username": "string", //Username do usuário
    "password": "string", //Password usada para autenticar o usuário
    "name": {
      //Nome do usuário
      "firstName": "string", //Primeiro nome
      "lastName": "string" //Último sobrenome
    },
    "address": {
      //Endereço do usuário
      "city": "string", //Nome da cidade
      "street": "string", //Logradouro
      "number": 0, //Número do endereço
      "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
      "geolocation": {
        //Geolocalização do endereço
        "lat": "string", //Latitude
        "long": "string" //Longitude
      }
    },
    "phone": "string", //Telefone no formato internacional
    "status": "string", //Status do usuário. Valores aceitos: Active, Inactive, Suspended
    "role": "string" //Função do usuário. Valores aceitos:Customer, Manager, Admin
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
      //dados do usuário caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "email": "string", //Email do usuário.
      "username": "string", //Username do usuário
      "password": "string", //Password usada para autenticar o usuário
      "name": {
        //Nome do usuário
        "firstName": "string", //Primeiro nome
        "lastName": "string" //Último sobrenome
      },
      "address": {
        //Endereço do usuário
        "city": "string", //Nome da cidade
        "street": "string", //Logradouro
        "number": 0, //Número do endereço
        "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
        "geolocation": {
          //Geolocalização do endereço
          "lat": "string", //Latitude
          "long": "string" //Longitude
        }
      },
      "phone": "string", //Telefone no formato internacional
      "status": "string", //Status do usuário
      "role": "string" //Função do usuário
    }
  }
  ```

- **PUT** _/api/users/{id}_ - Atualiza os dados de um usuário cadastrado

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do usuário a ser atualizado.
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário a ser atualizado
    "email": "string", //Email do usuário. Deve ser único
    "username": "string", //Username do usuário
    "password": "string", //Password usada para autenticar o usuário
    "name": {
      //Nome do usuário
      "firstName": "string", //Primeiro nome
      "lastName": "string" //Último sobrenome
    },
    "address": {
      //Endereço do usuário
      "city": "string", //Nome da cidade
      "street": "string", //Logradouro
      "number": 0, //Número do endereço
      "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
      "geolocation": {
        //Geolocalização do endereço
        "lat": "string", //Latitude
        "long": "string" //Longitude
      }
    },
    "phone": "string", //Telefone no formato internacional
    "status": "string", //Status do usuário. Valores aceitos: Active, Inactive, Suspended
    "role": "string" //Função do usuário. Valores aceitos:Customer, Manager, Admin
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
      //dados do usuário caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      //dados do usuário caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "email": "string", //Email do usuário.
      "username": "string", //Username do usuário
      "password": "string", //Password usada para autenticar o usuário
      "name": {
        //Nome do usuário
        "firstName": "string", //Primeiro nome
        "lastName": "string" //Último sobrenome
      },
      "address": {
        //Endereço do usuário
        "city": "string", //Nome da cidade
        "street": "string", //Logradouro
        "number": 0, //Número do endereço
        "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
        "geolocation": {
          //Geolocalização do endereço
          "lat": "string", //Latitude
          "long": "string" //Longitude
        }
      },
      "phone": "string", //Telefone no formato internacional
      "status": "string", //Status do usuário
      "role": "string" //Função do usuário
    }
  }
  ```

- **DELETE** _/api/users/{id}_ - Remove os dados de um usuário cadastrado

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do usuário a ser removido.
    - _**BODY**_ : nenhum

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
    ]
  }
  ```

- **GET** _/api/users_ - Retorna os usuários cadastrados de forma paginada

  - Parâmetros de entrada

    - _**QUERY**_ :
      - _page - \_inteiro_ - Número da página desejada (Default: 1)
      - _size - \_inteiro_ - Quantidade de registros por página (Default: 10)
      - _order - \_string_ - Define a ordenação do resultado
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : nenhum

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
    "data": [
      //lista dos usuários encontrados caso tenha sucesso na operação
      {
        //dados do usuário
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
        "email": "string", //Email do usuário.
        "username": "string", //Username do usuário
        "password": "string", //Password usada para autenticar o usuário
        "name": {
          //Nome do usuário
          "firstName": "string", //Primeiro nome
          "lastName": "string" //Último sobrenome
        },
        "address": {
          //Endereço do usuário
          "city": "string", //Nome da cidade
          "street": "string", //Logradouro
          "number": 0, //Número do endereço
          "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
          "geolocation": {
            //Geolocalização do endereço
            "lat": "string", //Latitude
            "long": "string" //Longitude
          }
        },
        "phone": "string", //Telefone no formato internacional
        "status": "string", //Status do usuário
        "role": "string" //Função do usuário
      }
    ],
    "currentPage": 0, //Página retornada
    "totalPages": 0, //Quantidade total de páginas disponíveis
    "totalCount": 0 //Quantidade total de usuários encontrados
  }
  ```

- **GET** _/api/users/{id}_ - Pesquisa um usuário pelo seu identificador

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do usuário a ser pesquisado.
    - _**BODY**_ : nenhum

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
      //dados do usuário caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "email": "string", //Email do usuário.
      "username": "string", //Username do usuário
      "password": "string", //Password usada para autenticar o usuário
      "name": {
        //Nome do usuário
        "firstName": "string", //Primeiro nome
        "lastName": "string" //Último sobrenome
      },
      "address": {
        //Endereço do usuário
        "city": "string", //Nome da cidade
        "street": "string", //Logradouro
        "number": 0, //Número do endereço
        "zipCode": "string", //Código postal no formato brasileiro (XXXXX-XXX)
        "geolocation": {
          //Geolocalização do endereço
          "lat": "string", //Latitude
          "long": "string" //Longitude
        }
      },
      "phone": "string", //Telefone no formato internacional
      "status": "string", //Status do usuário
      "role": "string" //Função do usuário
    }
  }
  ```
