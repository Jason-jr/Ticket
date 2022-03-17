namespace Application.Models;

public class KeyValueVM<TKey>
{
    public TKey Id { get; set; }

    public string Value { get; set; }
}