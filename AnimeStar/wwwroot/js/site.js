
$('#add-comment-form').submit(function (event) {
    event.preventDefault(); // Предотвращаем отправку формы по умолчанию
    var forumId = $('input[name="forumId"]').val();
    // Отправляем данные формы на сервер с помощью AJAX
    $.ajax({
        url: '/Anime/AddComment',
        type: 'POST',
        data: $(this).serialize() + "&forumId=" + forumId, // Сериализуем данные формы
        success: function (data) {
            // Обновляем разметку страницы, добавляя новый комментарий
            $('#comments-container').append(data);
            $('#comment').val('');
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

$('#add-review-form').submit(function (event) {
    event.preventDefault();

    $.ajax({
        url: '/Anime/AddReview',
        type: 'POST',
        data: $(this).serialize(),
        success: function (data) {
            $('#review-container').prepend(data); // Добавление новой рецензии в начало списка
            $('#comment').val('');
        },
        error: function () {
            console.log('Произошла ошибка при отправке комментария.');
        }
    });
});

$('#add-forum-form').submit(function (event) {
    event.preventDefault(); // Предотвращаем отправку формы по умолчанию

    // Сохраняем ссылку на текущий контекст (this), чтобы использовать его внутри функции обратного вызова
    var $form = $(this);

    // Отправляем данные формы на сервер с помощью AJAX
    $.ajax({
        url: '/Anime/AddForum',
        type: 'POST',
        data: $form.serialize(), // Сериализуем данные формы
        success: function () {
            window.location.reload();
        },
        error: function () {
            // Обрабатываем ошибку, если таковая возникла
            console.log('Произошла ошибка при создании форума.');
        }
    });
});

document.addEventListener("DOMContentLoaded", function () {
    var animeType = document.getElementById("TypeOfAnime").value;
    var filmLengthField = document.getElementById("filmLengthField");

    if (animeType === "Фильм") {
        filmLengthField.style.display = "block";
    } else {
        filmLengthField.style.display = "none";
    }

    document.getElementById("TypeOfAnime").addEventListener("change", toggleFilmLengthField);
});

function toggleFilmLengthField() {
    var animeType = document.getElementById("TypeOfAnime").value;
    var filmLengthField = document.getElementById("filmLengthField");

    if (animeType === "Фильм") {
        filmLengthField.style.display = "block";
    } else {
        filmLengthField.style.display = "none";
    }
}

async function deleteComment(commentId) {
    try {
        const response = await fetch(`/Anime/DeleteComment?commentId=${commentId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest' // Для ASP.NET Core маркировки запроса как AJAX
            },
        });

        if (response.ok) {
            window.location.reload();
            // После удаления обновите интерфейс, скрывая удаленный комментарий
        } else {
            console.error('Failed to delete comment');
        }
    } catch (error) {
        console.error('Error occurred while deleting comment:', error);
    }
}

async function deleteForum(forumId) {
    try {
        const response = await fetch(`/Anime/DeleteForum?forumId=${forumId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest' // Для ASP.NET Core маркировки запроса как AJAX
            },
        });

        if (response.ok) {
            window.location.reload();
            // После удаления обновите интерфейс, скрывая удаленный форум
        } else {
            console.error('Failed to delete forum');
        }
    } catch (error) {
        console.error('Error occurred while deleting forum:', error);
    }
}

async function deleteTextReview(reviewId) {
    try {
        const response = await fetch(`/Anime/DeleteTextReview?reviewId=${reviewId}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'X-Requested-With': 'XMLHttpRequest' // Для ASP.NET Core маркировки запроса как AJAX
            },
        });

        if (response.ok) {
            window.location.reload();
            // После удаления обновите интерфейс, скрывая удаленный форум
        } else {
            console.error('Failed to delete review');
        }
    } catch (error) {
        console.error('Error occurred while deleting review:', error);
    }
}

