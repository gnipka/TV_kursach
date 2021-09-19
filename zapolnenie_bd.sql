USE TV;

-- склады
INSERT INTO storage VALUES(1, 'Склад сырья и материалов №1');
INSERT INTO storage VALUES(2, 'Склад сырья и материалов №2');
INSERT INTO storage VALUES(3, 'Цеховая кладовая');
INSERT INTO storage VALUES(4, 'Склад готовой продукции');

-- РЦ
INSERT INTO work_center VALUES (1, 'Доступен', 480);
INSERT INTO work_center VALUES (2, 'Доступен', 600);
INSERT INTO work_center VALUES (3, 'Доступен', 480);
INSERT INTO work_center VALUES (4, 'Доступен', 480);
INSERT INTO work_center VALUES (5, 'Доступен', 1440);

-- спецификации
INSERT INTO specification VALUES (0, 'Спецификация телевизора');
INSERT INTO specification VALUES (1, 'Спецификация корпуса');
INSERT INTO specification VALUES (2, 'Спецификация плазменной модели');
INSERT INTO specification VALUES (3, 'Спецификация платы');
INSERT INTO specification VALUES (4, 'Спецификация пульта');

-- технологические карты
INSERT INTO technological_map VALUES (0, 'Производство телевизора');
INSERT INTO technological_map VALUES (1, 'Производство корпуса');
INSERT INTO technological_map VALUES (2, 'Производство плазменной модели');
INSERT INTO technological_map VALUES (3, 'Производство платы');
INSERT INTO technological_map VALUES (4, 'Производство пульта');


-- состав тех.карты
INSERT INTO map_composition (Operation_ID, Operation_desc, Processing_time, Map_ID, WC_ID) VALUES (0, 'Сборка телевизора', 60, 0, 5);
INSERT INTO map_composition (Operation_ID, Operation_desc, Processing_time, Map_ID, WC_ID) VALUES (1, 'Сборка корпуса', 30, 1, 1);
INSERT INTO map_composition (Operation_ID, Operation_desc, Processing_time, Map_ID, WC_ID) VALUES (2, 'Сборка плазменной модели', 30, 2, 2);
INSERT INTO map_composition (Operation_ID, Operation_desc, Processing_time, Map_ID, WC_ID) VALUES (3, 'Сборка платы', 15, 3, 3);
INSERT INTO map_composition (Operation_ID, Operation_desc, Processing_time, Map_ID, WC_ID) VALUES (4, 'Сборка пульта', 10, 4, 4);

-- номенклатура
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (0, 'Телевизионный тюнер');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (1, 'Декодер');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (2, 'Ячейки с плазмой');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (3, 'Модуль задней подсветки');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (4, 'Стекло');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (5, 'Слой диэлектрика');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (6, 'Разъемы');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (7, 'Блок управления');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (8, 'Динамики');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (9, 'Блок питания');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (10, 'Плата для пульта');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (11, 'Схема с клавишами');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (12, 'Транзистор');
INSERT INTO nomenclature (Nom_ID, Nom_description) VALUES (13, 'Резонатор');

INSERT INTO nomenclature (Nom_ID, Nom_description, Spec_ID, Map_ID) VALUES (14, 'Корпус в сборе', 1, 1);
INSERT INTO nomenclature (Nom_ID, Nom_description, Spec_ID, Map_ID) VALUES (15, 'Плазменная модель', 2, 2);
INSERT INTO nomenclature (Nom_ID, Nom_description, Spec_ID, Map_ID) VALUES (16, 'Плата', 3, 3);
INSERT INTO nomenclature (Nom_ID, Nom_description, Spec_ID, Map_ID) VALUES (17, 'Пульт', 4, 4);
INSERT INTO nomenclature (Nom_ID, Nom_description, Spec_ID, Map_ID) VALUES (18, 'Телевизор', 0, 0);

-- запасы
-- склад 1
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (0, 1, 10); -- Телевизионный тюнер
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (1, 1, 10); -- Декодер
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (2, 1, 10); -- Ячейки с плазмой
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (3, 1, 10); -- Модуль задней подсветки
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (4, 1, 10); -- Стекло
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (5, 1, 10); -- Слой диэлектрика
-- склад 2
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (6, 2, 20); -- Разъемы
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (7, 2, 20); -- Блок управления
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (8, 2, 20); -- Динамики
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (9, 2, 20); -- Блок питания
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (10, 2, 20); -- Плата для пульта
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (11, 2, 20); -- Схема с клавишами
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (12, 2, 20); -- Транзистор
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (13, 2, 20); -- Резонатор
-- цеховая кладовая
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (14, 3, 6); -- Корпус в сборе
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (15, 3, 8); -- Плазменная модель
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (16, 3, 10); -- Плата
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (17, 3, 5); -- Пульт
-- склад готовой продукции
INSERT INTO stocks (Nom_ID, Storage_ID, St_Count) VALUES (18, 4, 20); -- Телевизор


-- состав спецификаций
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (0, 'Сборка частей Телевизора', 0, 1, 0, 14);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (1, 'Сборка частей Телевизора', 0, 1, 0, 15);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (2, 'Сборка частей Телевизора', 0, 1, 0, 16);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (3, 'Сборка частей Телевизора', 0, 1, 0, 17);

INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (4, 'Сборка корпуса', 1, 1, 1, 6);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (5, 'Сборка корпуса', 1, 1, 1, 7);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (6, 'Сборка корпуса', 1, 1, 1, 8);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (7, 'Сборка корпуса', 1, 1, 1, 9);

INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (8, 'Сборка плазменной модели', 2, 1, 2, 2);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (9, 'Сборка плазменной модели', 2, 1, 2, 3);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (10, 'Сборка плазменной модели', 2, 1, 2, 4);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (11, 'Сборка плазменной модели', 2, 1, 2, 5);

INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (12, 'Сборка платы', 3, 1, 3, 0);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (13, 'Сборка платы', 3, 1, 3, 1);

INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (14, 'Сборка пульта', 4, 1, 4, 10);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (15, 'Сборка пульта', 4, 1, 4, 11);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (16, 'Сборка пульта', 4, 1, 4, 12);
INSERT INTO spec_composition (Comp_ID, Comp_description, Operation_ID, Comp_Count, Spec_ID, Nom_ID) VALUES (17, 'Сборка пульта', 4, 1, 4, 13);





