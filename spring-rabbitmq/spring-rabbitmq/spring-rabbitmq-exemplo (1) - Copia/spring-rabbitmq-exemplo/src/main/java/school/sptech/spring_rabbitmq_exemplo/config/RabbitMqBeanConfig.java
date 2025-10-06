// src/main/java/school/sptech/spring_rabbitmq_exemplo/config/RabbitMqBeanConfig.java
package school.sptech.spring_rabbitmq_exemplo.config;

import org.springframework.amqp.core.*;
import org.springframework.amqp.rabbit.connection.ConnectionFactory;
import lombok.RequiredArgsConstructor;
import org.springframework.amqp.rabbit.core.RabbitTemplate;
import org.springframework.amqp.support.converter.Jackson2JsonMessageConverter;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

@Configuration

public class RabbitMqBeanConfig {
    private final ConnectionFactory connectionFactory;

    @Value("${broker.exchange.name}")
    private String exchangeName;

    @Value("${broker.queue.name}")
    private String queueName;

    @Value("${broker.routing.key.name.message}")
    private String routingKeyNameMessage;

    @Value("${broker.routing.key.name.bind}")
    private String routingKeyNameBind;

    public RabbitMqBeanConfig(ConnectionFactory connectionFactory) {
        this.connectionFactory = connectionFactory;
    }

    @Bean
    public TopicExchange exchange() {
        return new TopicExchange(exchangeName);
    }

    @Bean
    public Queue queue() {
        return new Queue(queueName, true);
    }

    @Bean
    public Binding binding(Queue queue, TopicExchange exchange) {
        return BindingBuilder
                .bind(queue)
                .to(exchange)
                .with(routingKeyNameBind);
    }

    @Bean
    public RabbitTemplate rabbitTemplate() {
        RabbitTemplate rabbitTemplate = new RabbitTemplate(connectionFactory);
        rabbitTemplate.setExchange(exchangeName);
        rabbitTemplate.setRoutingKey(routingKeyNameMessage);
        rabbitTemplate.setMessageConverter(new Jackson2JsonMessageConverter());
        return rabbitTemplate;
    }
}

