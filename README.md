<h1>AI Prompter</h1>

<span>Simple console app that allows you to prompt: 
- OpenAI API (API key required). Basic moderation for prompts is enabled.
- GPT4All (Data model required, available on https://gpt4all.io/index.html -> Model explorer). Model should be put in the same location as dockerfile and named "data_model.bin"

<h3>How to use it</h3>
1. Build docker image (docker build -t ai_prompter .)
2. Run docker image (docker run -i ai_prompter)
3. Enter prompt