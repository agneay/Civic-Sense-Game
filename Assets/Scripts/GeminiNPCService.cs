using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

public class GeminiNPCService : MonoBehaviour
{
    [Header("DEV ONLY – Do not ship API keys")]
    [SerializeField] private string apiKey;

    private const string MODEL_URL =
        "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=";

    public async Task<string> GetNPCResponse(string prompt)
    {
        string url = MODEL_URL + apiKey;

        string jsonBody =
@"{
  ""contents"": [
    {
      ""parts"": [
        { ""text"": """ + EscapeJson(prompt) + @""" }
      ]
    }
  ]
}";

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody));
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Gemini API Error: " + request.error);
            return "…The NPC stays silent.";
        }
        Debug.Log("Gemini RAW RESPONSE:\n" + request.downloadHandler.text);
        return ExtractText(request.downloadHandler.text);
    }

    private string EscapeJson(string text)
    {
        return text.Replace("\\", "\\\\")
                   .Replace("\"", "\\\"")
                   .Replace("\n", "\\n");
    }

    [System.Serializable]
    private class GeminiResponse
    {
        public Candidate[] candidates;
    }

    [System.Serializable]
    private class Candidate
    {
        public Content content;
    }

    [System.Serializable]
    private class Content
    {
        public Part[] parts;
    }

    [System.Serializable]
    private class Part
    {
        public string text;
    }

    private string ExtractText(string json)
    {
        try
        {
            GeminiResponse response =
                JsonUtility.FromJson<GeminiResponse>(json);

            if (response.candidates != null &&
                response.candidates.Length > 0 &&
                response.candidates[0].content.parts.Length > 0)
            {
                return response.candidates[0].content.parts[0].text;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON Parse Error: " + e.Message);
        }

        return "…The NPC stays silent.";
    }
}