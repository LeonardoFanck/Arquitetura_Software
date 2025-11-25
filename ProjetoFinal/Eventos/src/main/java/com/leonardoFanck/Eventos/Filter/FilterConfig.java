package com.leonardoFanck.Eventos.Filter;

import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.boot.web.servlet.FilterRegistrationBean;

@Configuration
public class FilterConfig {

    @Bean
    public FilterRegistrationBean<TokenFilter> tokenFilter() {
        FilterRegistrationBean<TokenFilter> reg = new FilterRegistrationBean<>();

        reg.setFilter(new TokenFilter());

        // protege apenas certificados (adicione mais rotas se quiser)
        reg.addUrlPatterns("/certificados/*");

        return reg;
    }
}