from flask import Flask, request, jsonify
from flask_cors import CORS
import os
from openai import OpenAI
from dotenv import load_dotenv

load_dotenv()
app = Flask(__name__)
# Permite requisições do seu front-end Angular (http://localhost:4200)
CORS(app, resources={r"/api/*": {"origins": "http://localhost:4200"}})

OPENAI_API_KEY = os.environ.get("OPENAI_API_KEY")

# Inicializa o cliente da OpenAI
client = OpenAI(api_key=OPENAI_API_KEY)

@app.route("/api/generate-content", methods=["POST"])
def generate_content():
    try:
        data = request.get_json()
        topic = data.get("topic")

        if not topic:
            return jsonify({"error": "Tópico não fornecido"}), 400

        # Faz a chamada para a API de chat completions da OpenAI
        response = client.chat.completions.create(
            model="gpt-3.5-turbo",  # Pode usar "gpt-4" se tiver acesso
            messages=[
                {"role": "system", "content": "Você é um assistente de estudos. Sua tarefa é gerar conteúdo detalhado e bem estruturado sobre um tópico específico."},
                {"role": "user", "content": f"Gere um texto de estudo completo e detalhado sobre o subtópico \"{topic}\". Inclua os conceitos principais, exemplos práticos e a importância do tema. O texto deve ser formatado usando Markdown para melhor leitura."}
            ],
            temperature=0.7
        )
        
        # Extrai o conteúdo da resposta
        generated_text = response.choices[0].message.content

        return jsonify({"content": generated_text})

    except Exception as e:
        return jsonify({"error": f"Erro na comunicação com a API da OpenAI: {e}"}), 500

@app.route("/api/adapt-pdf-content", methods=["POST"])
def adapt_pdf_content():
    try:
        data = request.get_json()
        pdf_text = data.get("pdf_text")

        if not pdf_text:
            return jsonify({"error": "Texto do PDF não fornecido"}), 400

        # Faz a chamada para a API de chat completions da OpenAI
        response = client.chat.completions.create(
            model="gpt-3.5-turbo",  # Pode usar "gpt-4" se tiver acesso
            messages=[
                {"role": "system", "content": "Você é um assistente de estudos. Sua tarefa é adaptar o conteúdo de um PDF para torná-lo mais compreensível e didático."},
                {"role": "user", "content": f"Adapte o seguinte conteúdo do PDF para torná-lo mais didático e fácil de entender, mantendo os conceitos principais: {pdf_text}"}
            ],
            temperature=0.7
        )
        
        # Extrai o conteúdo da resposta
        adapted_text = response.choices[0].message.content

        return jsonify({"adapted_content": adapted_text})

    except Exception as e:
        return jsonify({"error": f"Erro na comunicação com a API da OpenAI: {e}"}), 500

if __name__ == "__main__":
    app.run(debug=True, port=5000)