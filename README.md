## Сервис организации мероприятий "EventPad"
Это веб-сервис, состоящий из клиентского приложения на Blazor WebAssembly, серверной
части на ASP.Net WebApi, базы данных PostgreSQL.
Сервис позволяет:
* создавать мероприятия, например тренировка по волейболу или концерт,
* приобретать билеты на них
* учитывать их посещения

## Содержание
- [Технологии](#технологии)
- [Архитектура](#арихитектура)
- [Начало работы](#начало-работы)
- [Разработка](#разработка)
- [Тестирование](#тестирование)
- [To do](#to-do)
- [Команда проекта](#команда-проекта)

## Технологии
- ASP.Net Web.Api
- Blazor WebAssembly
- Http(s), Rest
- Компоненты: Entity Framework (code first), Serilog, Swagger, AutoMapper, FluentValidation, IdentityServer
- Сторонние сервисы: PostgreSQL, RabbitMQ, Redis, Docker, Docker Compose

## Архитектура
- Микро-сервис "EventPad.API"
- Микро-сервис "PayMS"
- Микро-сервис "Worker"
- Веб-клиент на Blazor WebAssembly
- База данных PostgerSQL
- Взаимодействие между микро-сервисами осуществляется при помощи RabbitMQ и Redis.
- Для авторизации используется JWT и Duende IdentityServer

![Image](https://github.com/MYP4/EventPad/blob/main/DataBase.jpg)

## Начало работы
Клонируйте репозиторий на свой компьютер:
```cmd
git clone https://github.com/MYP4/EventPad.git
```

Заполните appsettings.json в Worker/Systems для отправки email:
```cmd
  "EmailSender": {
    "Host": "",
    "Port": "465",
    "UseSsl": "true",
    "Email": "",
    "SenderName": "",
    "Password": ""
  }
```

Зайдите в папку прокта и выполните команды:
```cmd
docker-compose build
```
```cmd
docker-compose up
```

Демонстрационные аккаунты:

```cmd
Administrator:
Login: Admin@adm.com
Password: 123456
```
```cmd
Regular user:
Login: Petrov@pad.com
Password: qwertyui
```
```cmd
Regular user:
Login: a.olga@pad.com
Password: 12345678
```
```cmd
Regular user:
Login: GusevMaks@pad.com
Password: MaksGus
```


## Разработка


### Зачем вы разработали этот проект?
Проект был разработан в рамках курсовой работы для DSR NetSchool и Воронежского государственного университета.

## To do
- [ ] Написать тесты для сервиса
- [ ] Добавить внешний сервис оплаты
- [ ] Переписать front-end с Blazor на React
- [ ] Написать документацию
- [ ] Дописать сервис до Production


## Команда проекта
- Кононов Игорь — Full-Stack Developer
    
