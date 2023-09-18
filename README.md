# Rinha Compiler

Tree-Walking Interpreter implemented in CSharp using Doubly Linked List (why not?!)


![Nuget](https://img.shields.io/nuget/v/rinha)


## Build and Run

- build
```bash
docker build -t rinha .
```

- fib
```bash
docker run -it rinha files/fib.json
```

- sum
```bash
docker run -it rinha files/sum.json
```

- combination
```bash
docker run -it rinha files/combination.json
```

- helloworld
```bash
docker run -it rinha files/helloworld.json
```

## How to use

You don't need to download the source code to run this Interpreter, you can install it in your machine using the dotnet tool.
```
dotnet tool install -g Rinha
```

You can run the command
```
rinha <your file>
```

## Interpreter Language Features

- [x] Call
- [x] Function
- [x] Let
- [x] Var
- [x] Int
- [x] Str
- [x] Binary
- [x] If
- [x] Print
- [x] First
- [x] Second
- [x] Bool
- [x] Tuple