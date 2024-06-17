# Projeto `Life Bank Auth`

# Orientações

<details>
  <summary><strong>‼️ Executar projeto</strong></summary><br />

1. Clone o repositório

- Use o comando: `git clone git@github.com:Mathluiz23/life-bank-auth.git`.
- Entre na pasta do repositório que você acabou de clonar:
  - `cd life-bank-auth`

2. Instale as dependências

- Entre na pasta `src/`.
- Execute o comando: `dotnet restore`.

</details>

<details>
  <summary><strong>🛠 Testes</strong></summary><br />

### Executando todos os testes

Para executar os testes com o .NET, execute o comando dentro do diretório do seu projeto `src/<project>` ou de seus testes `src/<project>.Test`!

```
dotnet test
```

### Executando um teste específico

Para executar um teste específico, basta executar o comando `dotnet test --filter Name~TestMethod1`.

</details>

# O Projeto

O projeto `Life Bank`, consiste em uma aplicação de um banco digital. Onde foi realizada uma `WEB API` que possui algumas rotas com autorização, outras com autorização baseada em `Claims` e também rotas anônimas.

A autenticação é realizada utilizando o método `JWT`, um Token que retornará para realização de requisições futuras com autorizações.

## Configurações de Projeto

<details>
  <summary>Foram adicionadas algumas configurações necessárias para trabalhar com autenticação e autorização na aplicação </summary><br />

Os pacotes estão inclusos no arquivo de configuração de projeto no diretório `life-bank-auth`.

`life-bank-auth.csproj`:

```csharp
    <PackageReference Include="Microsoft.AspNetCore.Authentication" Version="2.2.0" />
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="6.0.6" />
```

Para utilização desses processos, também foram realizadas configurações na classe `Program.cs`.

- Preenchimento do segredo `JWT` para preenchimento da chave `Signature`.
- Comando para uso de Autenticação.
- Comando para uso de Autorização.

</details>

## 1 - Criado serviço Gerador de Token

<details>
  <summary>Implementado função Generate() da classe TokenGenerator </summary><br />

Criado gerador de Token na pasta Services, com uma classe de responsabilidade única denominada TokenGenerator.cs.

Criado a função `Generate()`, que vai gerar um Token JWT e retornar um valor do tipo string, o Token.

</details>

<details>
  <summary>Implementado função AddClaims() da classe TokenGenerator </summary><br />

Quando criado o Token, foi necessário o preenchimento das `Claims` na propriedade Subject, onde existe a função `AddClaims()`, onde o objeto da pessoa cliente é passado como parâmetro.

Campos utilizados para preenchimento das `Claims`: Name, Currency e ClientType.

Os valores de `Claim` recebem uma `string`, vindo dos valores do `Client`

`ClientType` foi realizada a lógica de que:

se o valor da propriedade booleana do objeto `Client` denominada `IsCompany` for verdadeira, o valor preenchido para a `Claim` será uma `string` de `ClientTypeEnum.PessoaJuridica`. Se não, será `ClientTypeEnum.PessoaFisica`.

</details>

<details>
  <summary>Testes Unitários para Serviço Gerador de Token</summary><br />

Utilizado a classe TestTokenGenerator.cs para os testes do serviço gerador de Token

Para garantir eficiência na geração de Token, foram criados testes unitários para função TestTokenGeneratorSuccess() e `TestTokenGeneratorKeysSuccess()`.

A função `TestTokenGeneratorSuccess()` irá apenas validar que o retorno do serviço gerador de Token não é vazio ou nulo.

A `TestTokenGeneratorKeysSuccess()` verifica se o Token realmente respeita o formato `JWT`.

</details>

## 2 - Criado Endpoint Anônimo

<details>
  <summary>Implementado função CreateAccountUntilFriday() da classe HomeController </summary><br />

No `HomeController`, foi criada a função `CreateAccountUntilFriday()`, que não precisa de autorização.

- A rota dessa função é: `MessageForEveryone`.

- Retornará uma string com valor `Crie sua conta na Like Bank até sexta-feira!`.

</details>

<details>
  <summary>Testes para Endpoint Anônimo </summary><br />

Na classe `TestHomeController`, foi adicionado a função `TestMessageForEveryoneSuccess()`.

- Criado a função `ApiGetRequest()` da classe `WebApplication` para realização da requisição `HTTP` onde foi passado a rota `Home/MessageForEveryone`.

</details>

## 3 - Criado Endpoint Autorização

<details>
  <summary>Implementado função PlataformWelcome() da classe ClientController </summary><br />

No controlador `ClientController`, foi criado a função `PlataformWelcome()`, que precisará de autorização.

- A rota dessa função é: `PlataformWelcome`.

- Retornará uma string com valor `Que ótimo ter você aqui novamente, sinta-se a vontade!`.

</details>

<details>
  <summary>Testes para Endpoint com Autorização </summary><br />

Na classe `TestClientController`, criado a função `TestPlataformWelcomeSuccess()`.

- irá receber 3 parâmetros passados pelo atributo `[InlineData]`: Name, IsCompany e Currency.

- Verifica se monta um objeto de `Client`, utilizando parâmetros que foram informados.

- Irá enviar os dados para a geração de Token.

</details>

<details>
  <summary>Teste de Falha em função TestPlataformWelcomeFail()</summary><br />

Na classe `TestClientController`, criado a função `TestPlataformWelcomeFail()`.

- Informa o parâmetro utilizando o atributo `[InlineData]`: Token.

- O parâmetro passado é uma string aleatória para simular um Token Inválido.

</details>

## 4 - Criado Endpoint com Autorização baseada em `Claims`

<details>
  <summary>Implementado função NewPromoAlert() da classe ClientController </summary><br />

No controlador `ClientController`, criada função `NewPromoAlert()`, que precisará de autorização.

- A rota dessa função é: `NewPromoAlert`.

- Retornará uma string com valor `Aproveite a nova promoção da Life Bank agora mesmo!`.

- Tipo Get de requisição HTTP.

- Autorização aplicando política `NewPromo`:

1. A pessoa cliente deve ter como propriedade `Currency` a moeda `Real` ou `Peso`. Criado Enum(`CurrencyEnum`) para essa propriedade.
2. A pessoa cliente deve ser uma `Pessoa Física`, criado o Enum `ClientTypeEnum`.

</details>

<details>
  <summary>Testes para Endpoint com Autorização baseada em `Claims`</summary><br />

Na classe `TestClientController`, criado a função `TestNewPromoAlertSuccess()`.

- Passados três parâmetros no atributo `[InlineData]`: Name, IsCompany e Currency.

- Nesse caso, os parâmetros passados devem seguir a política criada:

1. O valor do campo `IsCompany` igual a `false`.
2. O valor de `Currency` igual a `Real` ou `Peso`.

- Irá montar objeto de `Client` de acordo com os parâmetros.

</details>

<details>
  <summary>Teste de Falha em função TestNewPromoAlertFail()</summary><br />

Na classe `TestClientController`, criado a função `TestNewPromoAlertFail()`.

- Passados três parâmetros no atributo `[InlineData]`: Name, IsCompany e Currency.

- Nesse caso, os parâmetros passados NÃO podem ser:

1. O valor do campo `IsCompany` igual a `false`.
2. O valor de `Currency` igual a `Real` ou `Peso`.

- Irá montar objeto de `Client` de acordo com os parâmetros.

</details>
