
# Calculadora de horas trabalhadas API

## Autores
Criado por Eduardo Klug, João Artur Ramos Belli e Mateus de Faria da Silva.

## Descrição
Esta API é responsável por calcular a quantidade de horas trabalhadascom base em inputs recebidos em formato de um objeto, como 
```json
{
  "Start": "08:00",
  "Finish": "17:00",
  "Breaks": [
    {
      "Start": "12:00",
      "Finish": "12:30"
    },
    {
      "Start": "15:00",
      "Finish": "15:15"
    }
  ]
}
```
a calculadora retorna a quantidade de horas trabalhadas.

## Funcionalidades
- **Endpoint principal**: Recebe um objeto:
```json
{
  "Start": "08:00",
  "Finish": "17:00",
  "Breaks": [
    {
      "Start": "12:00",
      "Finish": "12:30"
    },
    {
      "Start": "15:00",
      "Finish": "15:15"
    }
  ]
}
e calcula as horas trabalhadas.
- **Testes Unitários**: Garantem a integridade e o correto funcionamento da API.

## Tecnologias Utilizadas
- .NET Core 8
- C#
- xUnit para testes unitários

## Endpoints

### POST /api/v1/worked-hours-calculator/calculate
Recebe um objeto para calcular as horas trabalhadas.

#### Exemplo de Requisição
```json
{
  "Start": "08:00",
  "Finish": "17:00",
  "Breaks": [
    {
      "Start": "12:00",
      "Finish": "12:30"
    },
    {
      "Start": "15:00",
      "Finish": "15:15"
    }
  ]
}

#### Exemplo de Resposta
```json
{
  "08:00"
}

## Como Executar o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/JoaooArtur/hours-worked-calculator.git
   ```
   
2. Navegue até o diretório do projeto:
   ```bash
   cd seu-repositorio
   ```

3. Restaure as dependências do projeto:
   ```bash
   dotnet restore
   ```

4. Execute o projeto:
   ```bash
   dotnet run
   ```

## Executando Testes Unitários

Para rodar os testes unitários e verificar se tudo está funcionando corretamente, execute o comando:

```bash
dotnet test
```
