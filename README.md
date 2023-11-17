# DemoApi
БД - mongo

Это апи для сайта преподавателя начальной школы. В api реализованы расписание класса, лента новостей,
дополнительные материалы рекомендуемые к изучению, а так же авторизация и аутентефикация на основе jwt.

Добавление, редактирование и удаление расписания класса, лента новостей,дополнительных материалов рекомендуемых к изучению и пользователей происходит в админской панели, вынесенной в отдельный контроллер

  ## модели
  ###informationBLock - информация
        Guid Id - id
        string Title - заголовок 
        string Body - наполнение

  ###MaterialsFile - отдельный файл
        Guid Id - id
        string Title - название файла
        string Url - url файла

  ###MaterialsGroup - группы файлов(По типу "Русский", "Математика")
        Guid Id - id 
        string Title - Название категории файлов 
        List<MaterialsFile>? Files - список файлов, относящихся у этой категории
  
  ### News - новости 
        Guid Id - id
        string? Title - заголовок новости 
        DateTime CreatedAt - дата создания новости 
        string? Body - наполнение новости
        string? ImageUrl - url картинки для новости
  
  ###Schedule - все расписание 
        Guid Id - id
        int key - номер дня 
        List<ScheduleEvent> Body - предметы на этот день 

  ###ShcheduleEvent - один урок
        Guid Id 
        int FromTime
        int ToTime 
        string Name 
        ScheduleEventKind Kind

  ###ScheduleEventKind - типы уроков в расписании

  ###TaskElsement - модель одного задания

  ###TaskModel -дополнительные задания

  ### User - пользователи

  ###Kind - уровни доступа пользователей
    
