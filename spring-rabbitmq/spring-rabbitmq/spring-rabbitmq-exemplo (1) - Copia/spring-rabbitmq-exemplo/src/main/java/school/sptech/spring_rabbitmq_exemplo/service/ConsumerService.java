package school.sptech.spring_rabbitmq_exemplo.service;

import org.springframework.amqp.rabbit.annotation.RabbitListener;
import org.springframework.stereotype.Service;

@Service
public class ConsumerService {

    @RabbitListener(queues = "${broker.queue.name}")
    public void receive(String message) {
        System.out.println("Received message: " + message);
    }
}