В проекте используется ParrelSync для тестирования в нескольких инстансах редактора.
Мультиплеер тестировался через него.

Управление в редакторе:
WASD - перемещение
Space - спавн нпс
ЛКМ - выстрел стрелой
Вращение мышью - обзор
5 - удалить всех NPC

Управление на Android путем UI + гироскоп. Очистка NPC путем нажатия на кнопку в сцене. Стрельба - касанием по экрану в месте где нет UI.

По улучшениям много моментов:
- сделать более правильную проброску ссылок через некий EntryPoint, избавиться от FindObjectOfType;
- пытался сделать NPC_Manager только на сервере, но не получилось, нужно дальше разбираться с Mirror;
- много времени ушло на поиск проблемы когда Touch был поверх UI (а значит стрелять на надо). Изначально все норм работало, но в дальнейшем что то сломалось, так и не понял что именно. Пока подозрения на 2 компонента PlayerInput которые может конфликтовали как то. Пришлось переписать метод проверки UI через рейкаст и избавиться от 1го PlayerInput'а;
- добавить ограничения на кол-во стрел и NPC;
- использовать пул для спавна стрел и NPC;
- стрелы сейчас аттачатся к коллайдеру NPC, можно сделать чтобы аттачились именно в меш с учетом анимации (попала например в руку и там остается и движется вместе с анимацией руки);
- сделать ограничение на кол-во стрел в NPC;
- добавить баллистику стрелам (гравитацию).