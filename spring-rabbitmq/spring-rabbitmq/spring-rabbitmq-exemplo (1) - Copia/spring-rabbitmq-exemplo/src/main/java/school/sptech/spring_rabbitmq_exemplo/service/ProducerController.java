package school.sptech.spring_rabbitmq_exemplo.service;// src/main/java/school/sptech/spring_rabbitmq_exemplo/controller/ProducerController.java
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class ProducerController {

    private final RabbitTemplate rabbitTemplate;

    // Valores vindos do application.properties
    @Value("${broker.exchange.name}")
    private String exchangeName;

    @Value("${broker.routing.key.name.message}")
    private String routingKey;

    public ProducerController(RabbitTemplate rabbitTemplate) {
        this.rabbitTemplate = rabbitTemplate;
    }

    // Exemplo: envia um JSON qualquer para a fila
    @PostMapping("/publish")
    public ResponseEntity<?> publishMessage(@RequestBody Map<String, Object> payload) {
        // RabbitTemplate está configurado com Jackson2JsonMessageConverter, então podemos enviar um Map
        rabbitTemplate.convertAndSend(exchangeName, routingKey, payload);
        return ResponseEntity.status(201).body(Map.of("status", "sent", "payload", payload));
    }
}
