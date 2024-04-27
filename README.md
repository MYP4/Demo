## Сервис организации мероприятий "EventPad"
Это веб-сервис, состоящий из клиентского приложения на Blazor WebAssembly, серверной
части на ASP.Net WebApi, базы данных PostgreSQL.
Сервис позволяет:
* создавать мероприятия, например тренировка по волейболу или концерт,
* приобретать билеты на них
* учитывать их посещения

## Содержание
- [Технологии](#технологии)
- [Архитектура](#архитектура)
- [База данных](#база-данных)
- [Начало работы](#начало-работы)
- [ToDo](#todo)
- [Команда проекта](#команда-проекта)

## Технологии
- ASP.Net Web.Api
- Blazor WebAssembly
- Http(s), Rest
- Компоненты: Entity Framework (code first), Serilog, Swagger, AutoMapper, FluentValidation, IdentityServer
- Сторонние сервисы: PostgreSQL, RabbitMQ, Redis, Docker, Docker Compose

## Архитектура
- Веб-клиент на Blazor WebAssembly
- Основной backend "EventPad.API"
- Микро-сервис для работы с платежами "EventPad.PayMS"
- Микро-сервис для отправки сообщений "EventPad.Worker"
- База данных PostgerSQL
- Взаимодействие между микро-сервисами осуществляется при помощи RabbitMQ и Redis.
- Для авторизации используется JWT и Duende IdentityServer


## База данных
### База данных "EventPad.API"
![Image](https://github.com/MYP4/EventPad/blob/main/EventPad.Api.jpg)
### База данных "EventPad.PayMS"
![Image](https://github.com/MYP4/EventPad/blob/main/EventPad.PayMS.jpg)

## Начало работы
Клонируйте репозиторий на свой компьютер:
```cmd
git clone https://github.com/MYP4/EventPad.git
```

Заполните env.worker в папке проекта для отправки email:
```cmd
EmailSender__Host=
EmailSender__Port=465
EmailSender__UseSsl=true
EmailSender__Email=
EmailSender__SenderName=
EmailSender__Password=
```

Зайдите в папку прокта и выполните команды:
```cmd
docker-compose build
```
```cmd
docker-compose up
```

После запуска всех контейнеров откройте в браузере:
- Веб-клиент:
```
http://localhost:10003/
```
- API Swagger
```
http://localhost:10000/docs
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

## ToDo
- [ ] Поиск и отладка ошибок на frontend
- [ ] Написать тесты для сервиса
- [ ] Добавить внешний сервис оплаты
- [ ] Переписать frontend с Blazor на React
- [ ] Написать документацию
- [ ] Дописать сервис до Production


## Команда проекта
- Кононов Игорь — Full-Stack Developer
    
