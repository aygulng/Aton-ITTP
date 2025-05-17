# aton-ITTP
RESTful Web API для управления пользователями с полным набором CRUD-операций
## Доступные методы API
  #### Административные (требуют авторизации под Admin)
  - Создание пользователей
  - Просмотр всех активных пользователей 
  - Поиск пользователей по логину
  - Фильтрация по возрасту  
  - Удаление/восстановление пользователей
  #### Пользовательские
  - Обновление своего профиля 
  - Смена пароля  
  - Смена логина  
  - Просмотр своей информации

## Технологии
- .NET 9
- PostgreSQL
- Entity Framework Core
- Swagger/OpenAPI

## Настройка базы данных
- Создайте базу данных atontest
- Настройте подключение в appsettings.json:
```sh
      "ConnectionStrings": {
      "DefaultConnection": "Host=localhost;Port=5432;Database=atontest;Username=postgres;Password=admin"
    }  
```
## Запуск
- Применятся все pending миграции
- Создается администратор по умолчанию:
```sh
      Логин: Admin
      Пароль: Admin
```
- Откройте Swagger UI
  Перейдите по адресу: https://localhost:7124/swagger
