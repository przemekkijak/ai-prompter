namespace Gpt4All.Model;

public interface IGpt4AllModelFactory
{
    IGpt4AllModel LoadModel(string modelPath);
}
