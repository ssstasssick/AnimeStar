
$('#add-comment-form').submit(function (event) {
    event.preventDefault(); // Предотвращаем отправку формы по умолчанию

    // Отправляем данные формы на сервер с помощью AJAX
    $.ajax({
        url: '/Anime/AddComment',
        type: 'POST',
        data: $(this).serialize(), // Сериализуем данные формы
        success: function (data) {
            // Обновляем разметку страницы, добавляя новый комментарий
            $('#comments-container').append(data);
        },
        error: function () {
            // Обрабатываем ошибку, если таковая возникла
            alert('Произошла ошибка при отправке комментария.');
        }
    });
});

$(document).ready(function () {
    $('.star-rating__input').change(function () {
        var animeId = $('input[name="AnimeId"]').val(); // Получаем Id аниме из скрытого поля формы
        var rating = $(this).val(); // Получаем выбранную оценку

        // Отправляем данные оценки на сервер с помощью AJAX
        $.ajax({
            url: '/Anime/Rate',
            type: 'POST',
            data: { animeId: animeId, rating: rating }, // Передаем Id аниме и оценку на сервер
            success: function (data) {
                if (data.success) {
                    // В случае успеха обновляем разметку или выполняем другие действия
                    console.log('Оценка успешно сохранена');
                } else {
                    // Обрабатываем сообщение об ошибке или выполняем другие действия
                    console.log('Ошибка при сохранении оценки: ' + data.message);
                }
            },
            error: function () {
                // Обрабатываем ошибку, если таковая возникла
                console.log('Произошла ошибка при отправке оценки');
            }
        });
    });
});

$(document).ready(function () {
    $('.list-group-item').click(function () {
        var state = $(this).text().trim(); // Получаем выбранное состояние из текста элемента списка
        var animeId = $('input[name="AnimeId"]').val();

        // Отправляем данные состояния на сервер с помощью AJAX
        $.ajax({
            url: '/PersonalList/AddOrUpdatePersonalListItem',
            type: 'POST',
            data: { AnimeId: animeId, State: state }, // Передаем ID пользователя и состояние на сервер
            success: function (data) {
                if (data.success) {
                    // В случае успеха обновляем разметку или выполняем другие действия
                    console.log('Состояние успешно обновлено');
                } else {
                    // Обрабатываем сообщение об ошибке или выполняем другие действия
                    console.log('Ошибка при обновлении состояния: ' + data.message);
                }
            },
            error: function () {
                // Обрабатываем ошибку, если таковая возникла
                console.log('Произошла ошибка при отправке состояния');
            }
        });
    });
});

$(document).ready(function () {
    $('#accordionFlushExample').on('click', '.accordion-button', function () {
        var state = $(this).text().trim(); // Получаем текст кнопки
        var targetId = $(this).attr('data-bs-target'); // Получаем ID целевого элемента, куда будем загружать аниме

        // Отправляем AJAX запрос для загрузки аниме по состоянию
        $.ajax({
            url: '/PersonalList/GetAnimeByState', // Путь к методу контроллера
            type: 'POST',
            data: { state: state }, // Передаем ID пользователя и состояние
            success: function (data) {
                // Заменяем содержимое целевого элемента полученными данными
                $(targetId).html(data);
            },
            error: function () {
                console.log('Произошла ошибка при загрузке аниме');
            }
        });
    });
});








