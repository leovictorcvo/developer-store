# Sale

###### [Página inicial](../README.md)

### /api/sale

Esse endpoint é o responsável por consultar e gerenciar as vendas cadastradas. O usuário precisar estar autenticado para poder executar as operações. Caso o usuário tenha uma função (role) de _customer_, ele terá acesso somente às suas vendas.

- **POST** _/api/sale_ - Cria uma nova venda

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
    "branch": "string", //Identificador da filial
    "cartId": "3fa85f64-5717-4562-b3fc-2c963f66afa6" //Id do carrinho de origem
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
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda
      "createdAt": "2025-05-10T13:39:47.569Z", //Data da criação da venda
      "customer": {
        //Dados do cliente
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
        "name": "string" //Nome do cliente
      },
      "branch": "string", //Filial onde ocorreu a venda
      "totalAmount": 0, //Valor total da venda
      "items": [
        //Lista de items vendidos
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do item
          "product": {
            //Produto vendido
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
            "name": "string", //Nome do produto
            "unitPrice": 0 //Valor unitário no momento da venda
          },
          "isCancelled": true, //Indica se o item está cancelado ou não
          "quantity": 0, //Quantidade vendida
          "discount": 0, //Desconto concedido
          "totalAmount": 0 //Valor total do item
        }
      ]
    }
  }
  ```

- **PUT** _/api/sale/{id}_ - Atualiza os dados de um venda cadastrada

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id da venda a ser atualizada.
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "saleId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda a ser atualizada
    "customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
    "branch": "string", //Identificador da filial
    "cartId": "3fa85f64-5717-4562-b3fc-2c963f66afa6" //Id do carrinho de origem
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
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda
      "createdAt": "2025-05-10T13:39:47.569Z", //Data da criação da venda
      "customer": {
        //Dados do cliente
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
        "name": "string" //Nome do cliente
      },
      "branch": "string", //Filial onde ocorreu a venda
      "totalAmount": 0, //Valor total da venda
      "items": [
        //Lista de items vendidos
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do item
          "product": {
            //Produto vendido
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
            "name": "string", //Nome do produto
            "unitPrice": 0 //Valor unitário no momento da venda
          },
          "isCancelled": true, //Indica se o item está cancelado ou não
          "quantity": 0, //Quantidade vendida
          "discount": 0, //Desconto concedido
          "totalAmount": 0 //Valor total do item
        }
      ]
    }
  }
  ```

- **DELETE** _/api/sale/{id}_ - Remove os dados de uma venda cadastrada

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do venda a ser removida.
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

- **PATCH** _/api/sale/{id}/cancel-item/{itemId}_ - Cancela um item de uma venda

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ :
      - id - Id da venda a ser pesquisada.
      - itemId - Id do item a ser cancelado.
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
      //dados da venda caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda
      "createdAt": "2025-05-10T13:39:47.569Z", //Data da criação da venda
      "customer": {
        //Dados do cliente
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
        "name": "string" //Nome do cliente
      },
      "branch": "string", //Filial onde ocorreu a venda
      "totalAmount": 0, //Valor total da venda
      "items": [
        //Lista de items vendidos
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do item
          "product": {
            //Produto vendido
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
            "name": "string", //Nome do produto
            "unitPrice": 0 //Valor unitário no momento da venda
          },
          "isCancelled": true, //Indica se o item está cancelado ou não
          "quantity": 0, //Quantidade vendida
          "discount": 0, //Desconto concedido
          "totalAmount": 0 //Valor total do item
        }
      ]
    }
  }
  ```

- **GET** _/api/sale_ - Retorna as vendas cadastradas de forma paginada

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
      //lista das vendas encontradas caso tenha sucesso na operação
      {
        //dados da venda
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda
        "createdAt": "2025-05-10T13:39:47.569Z", //Data da criação da venda
        "customer": {
          //Dados do cliente
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
          "name": "string" //Nome do cliente
        },
        "branch": "string", //Filial onde ocorreu a venda
        "totalAmount": 0, //Valor total da venda
        "items": [
          //Lista de items vendidos
          {
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do item
            "product": {
              //Produto vendido
              "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
              "name": "string", //Nome do produto
              "unitPrice": 0 //Valor unitário no momento da venda
            },
            "isCancelled": true, //Indica se o item está cancelado ou não
            "quantity": 0, //Quantidade vendida
            "discount": 0, //Desconto concedido
            "totalAmount": 0 //Valor total do item
          }
        ]
      }
    ],
    "currentPage": 0, //Página retornada
    "totalPages": 0, //Quantidade total de páginas disponíveis
    "totalCount": 0 //Quantidade total de vendas encontrados
  }
  ```

- **GET** _/api/sale/{id}_ - Pesquisa uma venda pelo seu identificador

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id da venda a ser pesquisada.
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
      //dados da venda caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id da venda
      "createdAt": "2025-05-10T13:39:47.569Z", //Data da criação da venda
      "customer": {
        //Dados do cliente
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do cliente
        "name": "string" //Nome do cliente
      },
      "branch": "string", //Filial onde ocorreu a venda
      "totalAmount": 0, //Valor total da venda
      "items": [
        //Lista de items vendidos
        {
          "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do item
          "product": {
            //Produto vendido
            "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
            "name": "string", //Nome do produto
            "unitPrice": 0 //Valor unitário no momento da venda
          },
          "isCancelled": true, //Indica se o item está cancelado ou não
          "quantity": 0, //Quantidade vendida
          "discount": 0, //Desconto concedido
          "totalAmount": 0 //Valor total do item
        }
      ]
    }
  }
  ```
