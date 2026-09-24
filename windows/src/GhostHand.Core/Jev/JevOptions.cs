namespace GhostHand.Core.Jev;

public class JevOptions
{
    public string BaseUrl { get; set; } = "https://ai-gateway.vercel.sh";
    public string ModelId { get; set; } = "typesafe-ai/jev";
    public string? ApiKey { get; set; }
    public bool UseDirectApi { get; set; }
    public bool ZeroDataRetention { get; set; } = false;
    public int TimeoutSeconds { get; set; } = 30;
    public int MaxRetries { get; set; } = 4;
    public double DecisionConfidenceThreshold { get; set; } = 0.0;
    public double RiskConfidenceThreshold { get; set; } = 0.70;

    public static JevOptions FromEnvironment()
    {
        var options = new JevOptions();

        var directKey = Environment.GetEnvironmentVariable("TYPESAFE_API_KEY");
        if (string.IsNullOrWhiteSpace(directKey))
            directKey = Environment.GetEnvironmentVariable("JEV_API_KEY");

        if (!string.IsNullOrWhiteSpace(directKey))
        {
            options.UseDirectApi = true;
            options.ApiKey = directKey;
            options.BaseUrl = Environment.GetEnvironmentVariable("JEV_BASE_URL")?.TrimEnd('/')
                ?? "https://api.typesafe.ai";
            options.ModelId = "jev-latest";
        }

        var baseUrl = Environment.GetEnvironmentVariable("AI_GATEWAY_BASE_URL");
        if (!options.UseDirectApi && !string.IsNullOrWhiteSpace(baseUrl))
            options.BaseUrl = baseUrl;

        var model = Environment.GetEnvironmentVariable("JEV_MODEL");
        if (!options.UseDirectApi && !string.IsNullOrWhiteSpace(model))
            options.ModelId = model;

        var key = Environment.GetEnvironmentVariable("AI_GATEWAY_API_KEY");
        if (!options.UseDirectApi && !string.IsNullOrWhiteSpace(key))
            options.ApiKey = key;

        if (bool.TryParse(Environment.GetEnvironmentVariable("ZERO_DATA_RETENTION"), out var zeroRetention))
            options.ZeroDataRetention = zeroRetention;

        if (double.TryParse(Environment.GetEnvironmentVariable("DECISION_CONFIDENCE_THRESHOLD"), out var conf))
            options.DecisionConfidenceThreshold = conf;

        if (double.TryParse(Environment.GetEnvironmentVariable("RISK_CONFIDENCE_THRESHOLD"), out var riskConf))
            options.RiskConfidenceThreshold = riskConf;

        return options;
    }
}
