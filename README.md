# DemoApi
БД - mongo

Это апи для сайта преподавателя начальной школы. В api реализованы расписание класса, лента новостей,
дополнительные материалы рекомендуемые к изучению, а так же авторизация и аутентефикация на основе jwt.

Добавление, редактирование и удаление расписания класса, лента новостей,дополнительных материалов рекомендуемых к изучению и пользователей происходит в админской панели, вынесенной в отдельный контроллер

  ## модели
  ###informationBLock - информация

  ###MaterialsFile - отдельный файл

  ###MaterialsGroup - группы файлов(По типу "Русский", "Математика")

  ### News - новости 

  ###Schedule - все расписание 

  ###ShcheduleEvent - один урок

  ###ScheduleEventKind - типы уроков в расписании

  ###TaskElsement - модель одного задания

  ###TaskModel -дополнительные задания

  ### User - пользователи

  ###Kind - уровни доступа пользователей
    
