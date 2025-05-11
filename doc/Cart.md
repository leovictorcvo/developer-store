# Cart

###### [Página inicial](../README.md)

### /api/carts

Esse endpoint é o responsável por consultar e gerenciar os carrinhos cadastrados. O usuário precisar estar autenticado para poder executar as operações. Caso o usuário tenha uma função (role) de _customer_, ele terá acesso somente aos seus carrinhos.

- **POST** _/api/carts_ - Cria um novo carrinho

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
    "date": "2025-05-10T11:34:50.821Z", //Data em formato UTC da criação do carrinho
    "products": [
      //Lista de produtos
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
        "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
      }
    ]
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
      //dados do carrinho caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do carrinho
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "date": "2025-05-10T11:34:50.823Z", //Data em formato UTC da criação do carrinho
      "products": [
        //Lista de produtos
        {
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
          "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
        }
      ]
    }
  }
  ```

- **PUT** _/api/carts/{id}_ - Atualiza os dados de um carrinho cadastrado

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do carrinho a ser atualizado.
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do carrinho a ser atualizado
    "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
    "date": "2025-05-10T11:34:50.821Z", //Data em formato UTC da criação do carrinho
    "products": [
      //Lista de produtos
      {
        "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
        "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
      }
    ]
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
      //dados do carrinho caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do carrinho criado
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "date": "2025-05-10T11:34:50.823Z", //Data em formato UTC da criação do carrinho
      "products": [
        //Lista de produtos
        {
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
          "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
        }
      ]
    }
  }
  ```

- **DELETE** _/api/carts/{id}_ - Remove os dados de um carrinho cadastrado

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do carrinho a ser removido.
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

- **GET** _/api/carts_ - Retorna os carrinhos cadastrados de forma paginada

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
      //lista dos carrinhos encontrados caso tenha sucesso na operação
      {
        //dados do carrinho
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do carrinho
        "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
        "date": "2025-05-10T11:34:50.823Z", //Data em formato UTC da criação do carrinho
        "products": [
          //Lista de produtos
          {
            "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
            "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
          }
        ]
      }
    ],
    "currentPage": 0, //Página retornada
    "totalPages": 0, //Quantidade total de páginas disponíveis
    "totalCount": 0 //Quantidade total de carrinhos encontrados
  }
  ```

- **GET** _/api/carts/{id}_ - Pesquisa um carrinho pelo seu identificador

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do carrinho a ser pesquisado.
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
      //dados do carrinho caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do carrinho
      "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do usuário
      "date": "2025-05-10T11:34:50.823Z", //Data em formato UTC da criação do carrinho
      "products": [
        //Lista de produtos
        {
          "productId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
          "quantity": 0 //Quantidade de produtos (máximo de 20 items por produto)
        }
      ]
    }
  }
  ```
