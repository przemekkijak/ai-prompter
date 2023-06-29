<h1>AI Prompter</h1>

<span>Simple console app that allows you to prompt: 
- OpenAI API (API key required). Basic moderation for prompts is enabled.
- GPT4All (Data model required, available on https://gpt4all.io/index.html -> Model explorer). Model should be put in the same location as dockerfile and named "data_model.bin"
</span>

<h3>How to use it</h3>
<p> - Build docker image (docker build -t ai_prompter .) </p>
<p> - Run docker image (docker run -i ai_prompter) </p>
<p> - Enter prompt </p>
