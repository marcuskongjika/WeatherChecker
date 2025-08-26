import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import com.fasterxml.jackson.databind.JsonNode;
import java.util.Scanner;
import com.fasterxml.jackson.databind.ObjectMapper;

public class Main{
    public static void main(String[] args) throws IOException, InterruptedException {
        String API_KEY = args[0];
        String City = args[1];


        String requestURL = "http://api.openweathermap.org/data/2.5/weather?q=" +
                City +
                "&appid=" +
                API_KEY;

        var client = HttpClient.newHttpClient();
        var request = HttpRequest.newBuilder(URI.create(requestURL))
                .header("accept","application/json")
                .build();

        HttpResponse<String> response = client.send(request, HttpResponse.BodyHandlers.ofString());
        String json = response.body();
        ObjectMapper mapper = new ObjectMapper();
        JsonNode root = mapper.readTree(json);

        double temp = (root.path("main").path("temp").asDouble()) - 273.15;
        String condition = root.path("weather").get(0).path("description").asText();

        System.out.println(temp);
        System.out.println(condition);
    }
}