# WinForms Editor (Bridge, Flyweight, Builder, State)
Projeto C# (.NET 6) de exemplo para a disciplina, implementando os 4 padrões:
- Bridge: separa Shape (abstração) de IRenderer (implementação).
- Flyweight: FlyweightFactory reaproveita instâncias de Shape com estado intrínseco.
- Builder: HouseBuilder monta um conjunto de formas que representam uma casa.
- State: Editor muda comportamento de interação (Desenhar / Selecionar).

Como usar:
1. Abra o projeto no Visual Studio 2022/2023 (suporta .NET 6).
2. Build e run.
3. Use os botões para alternar modo, desenhar formas e adicionar uma casa.

Arquivos principais:
- Program.cs
- MainForm.cs
- Patterns/ (IRenderer, GdiRenderer, Shapes, FlyweightFactory, PlacedShape)
- Builder/ (Drawing, DrawingBuilder)
- State/ (EditorState)
- Editor.cs

Observações:
- O projeto é um exemplo didático; o código foca em deixar os padrões evidentes e reusáveis.
