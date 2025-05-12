# Products

###### [Página inicial](../README.md)

### /api/products

Esse endpoint é o responsável por consultar e gerenciar os produtos cadastrados. As operações de cadastrar, alterar e excluir serão exclusivas para usuários com a função _Admin_ ou _Manager_. Os usuários com função _Customer_ só poderão consultar os dados.

- **POST** _/api/products_ - Cria um novo produto

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : nenhum
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "title": "string", //Título. Deve conter entre 3 a 100 caracteres
    "price": 0, //Preço unitário
    "description": "string", //Descrição. Deve conter entre 3 a 300 caracteres
    "category": "string", //Categoria. Deve conter entre 3 a 100 caracteres
    "image": "string", //Imagem. Deve conter entre 3 a 100 caracteres
    "rating": {
      //Avalição do produto
      "rate": 0, //Nota média
      "count": 0 //Quantidade de avaliações
    }
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
      //dados do produto caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
      "title": "string", //Título
      "price": 0, //Preço unitário
      "description": "string", //Descrição
      "category": "string", //Categoria
      "image": "string", //Imagem
      "rating": {
        //Avalição do produto
        "rate": 0, //Nota média
        "count": 0 //Quantidade de avaliações
      }
    }
  }
  ```

- **PUT** _/api/products/{id}_ - Atualiza os dados de um produto cadastrado

  - Parâmetros de entrada
    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do produto a ser atualizado.
    - _**BODY**_ : (application/json)

  ```jsonc
  {
    "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto a ser atualizado
    "title": "string", //Título. Deve conter entre 3 a 100 caracteres
    "price": 0, //Preço unitário
    "description": "string", //Descrição. Deve conter entre 3 a 300 caracteres
    "category": "string", //Categoria. Deve conter entre 3 a 100 caracteres
    "image": "string", //Imagem. Deve conter entre 3 a 100 caracteres
    "rating": {
      //Avalição do produto
      "rate": 0, //Nota média
      "count": 0 //Quantidade de avaliações
    }
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
      //dados do produto caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
      "title": "string", //Título
      "price": 0, //Preço unitário
      "description": "string", //Descrição
      "category": "string", //Categoria
      "image": "string", //Imagem
      "rating": {
        //Avalição do produto
        "rate": 0, //Nota média
        "count": 0 //Quantidade de avaliações
      }
    }
  }
  ```

- **DELETE** _/api/products/{id}_ - Remove os dados de um produto cadastrado

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do produto a ser removido.
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

- **GET** _/api/products_ - Retorna os produtos cadastrados de forma paginada

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
      //lista dos produtos encontrados caso tenha sucesso na operação
      {
        //dados do produto
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
        "title": "string", //Título
        "price": 0, //Preço unitário
        "description": "string", //Descrição
        "category": "string", //Categoria
        "image": "string", //Imagem
        "rating": {
          //Avalição do produto
          "rate": 0, //Nota média
          "count": 0 //Quantidade de avaliações
        }
      }
    ],
    "currentPage": 0, //Página retornada
    "totalPages": 0, //Quantidade total de páginas disponíveis
    "totalCount": 0 //Quantidade total de produtos encontrados
  }
  ```

- **GET** _/api/products/{id}_ - Pesquisa um produto pelo seu identificador

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
    - _**ROUTE**_ : id - Id do produto a ser pesquisado.
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
      //dados do produto caso tenha sucesso na operação
      "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
      "title": "string", //Título
      "price": 0, //Preço unitário
      "description": "string", //Descrição
      "category": "string", //Categoria
      "image": "string", //Imagem
      "rating": {
        //Avalição do produto
        "rate": 0, //Nota média
        "count": 0 //Quantidade de avaliações
      }
    }
  }
  ```

- **GET** _/api/products/categories_ - Retorna uma lista contendo as categorias cadastradas

  - Parâmetros de entrada

    - _**QUERY**_ : nenhum
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
    "data": ["string"] //lista de categorias cadastradas
  }
  ```

- **GET** _/api/products/categories/{category}_ - Retorna os produtos cadastrados em uma categoria de forma paginada

  - Parâmetros de entrada

    - _**QUERY**_ :
      - _page - \_inteiro_ - Número da página desejada (Default: 1)
      - _size - \_inteiro_ - Quantidade de registros por página (Default: 10)
      - _order - \_string_ - Define a ordenação do resultado
    - _**ROUTE**_ : category - _string_ - Categoria a ser pesquisada
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
      //lista dos produtos encontrados caso tenha sucesso na operação
      {
        //dados do produto
        "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6", //Id do produto
        "title": "string", //Título
        "price": 0, //Preço unitário
        "description": "string", //Descrição
        "category": "string", //Categoria
        "image": "string", //Imagem
        "rating": {
          //Avalição do produto
          "rate": 0, //Nota média
          "count": 0 //Quantidade de avaliações
        }
      }
    ],
    "currentPage": 0, //Página retornada
    "totalPages": 0, //Quantidade total de páginas disponíveis
    "totalCount": 0 //Quantidade total de produtos encontrados
  }
  ```
