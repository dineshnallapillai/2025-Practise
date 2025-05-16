public class TextEditorMemento
{
    public string Text { get; }

    public TextEditorMemento(string text)
    {
        Text = text;
    }
}

public class TextEditor
{
    private string _content = string.Empty;

    public void Type(string words)
    {
        _content += words;
    }

    public string GetContent() => _content;

    public TextEditorMemento Save() => new TextEditorMemento(_content);

    public void Restore(TextEditorMemento memento)
    {
        _content = memento.Text;
    }
}
public class History
{
    private readonly Stack<TextEditorMemento> _history = new();

    public void Save(TextEditorMemento memento) => _history.Push(memento);

    public TextEditorMemento? Undo() => _history.Count > 0 ? _history.Pop() : null;
}

