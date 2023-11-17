# DemoApi
БД - mongo

Это апи для сайта преподавателя начальной школы. В api реализованы расписание класса, лента новостей,
дополнительные материалы рекомендуемые к изучению, а так же авторизация и аутентефикация на основе jwt.

Добавление, редактирование и удаление расписания класса, лента новостей,дополнительных материалов рекомендуемых к изучению и пользователей происходит в админской панели, вынесенной в отдельный контроллер

  ## модели
  **informationBLock** - информация
        Guid Id - id
        string Title - заголовок 
        string Body - наполнение

  **MaterialsFile** - отдельный файл
        Guid Id - id
        string Title - название файла
        string Url - url файла

  **MaterialsGroup** - группы файлов(По типу "Русский", "Математика")
        Guid Id - id 
        string Title - Название категории файлов 
        List<MaterialsFile>? Files - список файлов, относящихся у этой категории
  
  **News** - новости 
        Guid Id - id
        string? Title - заголовок новости 
        DateTime CreatedAt - дата создания новости 
        string? Body - наполнение новости
        string? ImageUrl - url картинки для новости
  
  **Schedule** - все расписание 
        Guid Id - id
        int key - номер дня 
        List<ScheduleEvent> Body - предметы на этот день 

  **ShcheduleEvent** - один урок
        Guid Id - id
        int FromTime - время начала урока
        int ToTime - время конца урока
        string Name - название урока
        ScheduleEventKind Kind - тип урока

  **ScheduleEventKind** - типы уроков в расписании
        Lesson - урок
        Activity - активность(Физра, труд)

  **TaskElsement** - модель одного задания
        Guid Id - id
        string Class_Name - название задания
        string Body - Само задание
        DateTime UntilDate - до какого числа надо сделать   

  **TaskModel** -дополнительные задания
        Guid TaskId - id
        string Title  - название предмета по которому назначено задание
        List<TaskElement> Tasks - список заданий 
        DateTime CreatedAt - когда задания были добавлены
        
  **User** - пользователи
        Guid Id - id
        string Login - логин 
        string Password -пароль
        Kind Kind -уровень доступа
        string first_name  -имя
        string last_Name -фамилия

  **Kind** - уровни доступа пользователей
        User - рядовой пользователь
        Admin - админ
    
