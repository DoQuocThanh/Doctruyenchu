window.setCookie = function (name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}
window.getCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
    }
    return null;
}
window.loadingFullPage = function () {
    let elementLoading = $('#loadingPage');
    let status = elementLoading.css('display');
    if (status == 'none') {
        elementLoading.css('display', 'flex');
        $('body').css('overflow', 'hidden');
    } else {
        elementLoading.css('display', 'none');
        $('body').css('overflow', 'unset');
    }
}



window.objConfigFont = [
    {
        name: 'roboto',
        value: "'Roboto Condensed', sans-serif",
    },
    {
        name: 'mooli',
        value: "'Mooli', sans-serif",
    },
    {
        name: 'patrick_hand',
        value: "'Patrick Hand', cursive"
    }
]

function eraseCookie(name) {
    document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
}

var prevScrollPos = window.pageYOffset;
var scrollThreshold = 500;

window.enableScroll = function () {
    window.onscroll = function () {
        if (window.innerWidth < 992) {
            var currentScrollPos = window.pageYOffset;
            const headerMobile = document.querySelector('.header-mobile')

            if (prevScrollPos > currentScrollPos) {
                headerMobile.classList.add('show-scroll')
                headerMobile.classList.remove('hide-scroll')
            } else {
                if (currentScrollPos > scrollThreshold) {
                    headerMobile.classList.add('hide-scroll')
                    headerMobile.classList.remove('show-scroll')
                }
            }

            prevScrollPos = currentScrollPos;
        }
    }
}

window.enableScroll()

function showFullTabContent() {
    const productDetailInfo = document.querySelector('.story-detail__top--desc')
    if (productDetailInfo) {
        productDetailInfo.classList.add('show-full')

        const productDetailInfoMore = document.querySelector('.info-more')
        if (productDetailInfoMore) {
            const more = productDetailInfoMore.querySelector('.info-more--more')
            more && more.classList.remove('active')

            const collapse = productDetailInfoMore.querySelector('.info-more--collapse')
            collapse && collapse.classList.add('active')
        }
    }
}

function collapseDescription() {
    const productDetailInfoTabContent = document.querySelector('.story-detail__top--desc')
    if (productDetailInfoTabContent) {
        productDetailInfoTabContent.classList.remove('show-full')

        const productDetailInfoMore = document.querySelector('.info-more')
        if (productDetailInfoMore) {
            const more = productDetailInfoMore.querySelector('.info-more--more')
            more && more.classList.add('active')

            const collapse = productDetailInfoMore.querySelector('.info-more--collapse')
            collapse && collapse.classList.remove('active')
        }
    }
}

const storyDetailTopImage = document.querySelector('.story-detail__top--image')
if (storyDetailTopImage) {
    const img = storyDetailTopImage.querySelector('img')

    if (img) {
        const storyDesc = document.querySelector('.story-detail__top--desc')
        if (storyDesc) {
            storyDesc.style.maxHeight = img.clientHeight + 'px'
        }
    }
}

document.addEventListener('click', function (e) {
    if (e.target.classList.contains('info-more--more') || e.target.closest('.info-more--more')) {
        showFullTabContent()
    }

    if (e.target.classList.contains('info-more--collapse') || e.target.closest('.info-more--collapse')) {
        collapseDescription()
    }
})

const settingBackground = $('.setting-background')
settingBackground.on('change', function (e) {
    window.setCookie('bg_color', $(this).val(), 1)
    window.location.reload()
})

$(document).ready(function () {
    const selectStoriesHot = $(".select-stories-hot")
    const wrapperSkeletonStoriesHot = $(".wrapper-skeleton")
    if (selectStoriesHot) {
        function handleChangeListHot(category_id) {
            fetch(route('get.list.story.hot'), {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'X-CSRF-TOKEN': window.SuuTruyen.csrfToken,
                },
                body: JSON.stringify({
                    category_id: category_id
                })
            })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        var html = $(data.html);
                        var list = $('.section-stories-hot__list:not(.wrapper-skeleton)', html);
                        $('.section-stories-hot__list:not(.wrapper-skeleton)').replaceWith(list);
                        wrapperSkeletonStoriesHot.addClass('d-none')
                    }
                })
                .catch(function (error) {
                    console.log(error);
                    if (error.status !== 500) {
                        let errorMessages = error.responseJSON.errors;
                    } else {
                        errorContent = error.responseJSON.message;
                    }
                })
        }

        selectStoriesHot.on('change', function (e) {
            const categoryId = $(this).val()

            $('.section-stories-hot__list').addClass('d-none')
            wrapperSkeletonStoriesHot.removeClass('d-none')

            handleChangeListHot(categoryId)
        })

        const themeMode = $(".theme_mode")
        if (themeMode) {
            themeMode.on('change', function (e) {
                let valueThemeMode = $(this).is(":checked") ? 'dark' : 'light'

                window.setCookie('bg_color', valueThemeMode, 1)
                if ($(this).is(":checked")) {
                    $("body").addClass('dark-theme')
                } else {
                    $("body").removeClass('dark-theme')
                }
                // window.location.reload()
            })
        }

        let x = setInterval(() => {
            const selectStoriesHot = document.querySelector('.select-stories-hot')
            if (!selectStoriesHot) {
                clearInterval(x)
            } else {
                const options = selectStoriesHot.querySelectorAll('option')

                let valueSelected = null
                options.forEach((option, index) => {
                    if (option.hasAttribute('selected')) {
                        valueSelected = option.getAttribute('value')
                    }
                    option.removeAttribute('selected')
                })
                // console.log(valueSelected);

                // $('.select-stories-hot option:selected').next().attr('selected', 'selected');
                if (valueSelected == null) {
                    $('.select-stories-hot option:first').next().attr('selected', 'selected');
                } else {
                    $(`.select-stories-hot option[value="${valueSelected}"]`).next().attr('selected', 'selected');
                }

                if ($(".select-stories-hot").val() != 'Tất cả') {
                    handleChangeListHot($(".select-stories-hot").val())
                } else {
                    $('.select-stories-hot option:selected').next().attr('selected', 'selected');
                }
            }

        }, 50000);
    }


})


$(document).ready(function () {
    // Khi click vào nút chat bot, hiển thị chat box
    $('#chatBotToggle').on('click', function () {
        $('#chatBox').fadeIn(); // Hiển thị chat box
        $('#chatBotButton').fadeOut(); // Ẩn nút chat bot
    });

    // Đóng chat box khi click nút "x"
    $('#closeChat').on('click', function () {
        $('#chatBox').fadeOut(); // Ẩn chat box
        $('#chatBotButton').fadeIn(); // Hiển thị lại nút chat bot
    });

    // Gửi tin nhắn
    $('#sendMessage').on('click', function () {
        var userMessage = $('#chatInput').val();
        if (userMessage.trim() !== '') {
            // Hiển thị tin nhắn người dùng
            $('.chat-body').append('<p class="user-message">' + userMessage + '</p>');

            // Gửi phản hồi bot (ví dụ đơn giản)
            setTimeout(function () {
                $('.chat-body').append('<p class="bot-message">Bot: ' + "Tôi là Bot, bạn cần gì?" + '</p>');
                $('.chat-body').scrollTop($('.chat-body')[0].scrollHeight); // Cuộn xuống cuối tin nhắn
            }, 500);

            $('#chatInput').val(''); // Xóa nội dung ô nhập
        }
    });

    // Cập nhật khi nhấn Enter
    $('#chatInput').on('keypress', function (e) {
        if (e.which === 13) { // Khi nhấn Enter
            $('#sendMessage').click();
        }
    });


});

$(document).ready(function () {
    // Hàm tạo màu sắc ngẫu nhiên theo HSL (Hue, Saturation, Lightness)
    function generateRandomColor(id) {
        const hue = (id * 137) % 360;  // Nhân với một số để tạo ra sự khác biệt
        const saturation = 90 + (id % 10);  // Độ bão hòa (Saturation)
        const lightness = 30 + (id % 10);  // Độ sáng (Lightness)

        return `hsl(${hue}, ${saturation}%, ${lightness}%)`;
    }

    // Áp dụng màu cho các liên kết dựa trên @genre.Id
    $('.unique-category-name').each(function () {
        const genreId = $(this).data('genre-id');  // Lấy ID của genre từ thuộc tính data
        const color = generateRandomColor(genreId);  // Tạo màu cho mỗi genreId
        $(this).css('color', color);  // Áp dụng màu cho phần tử
    });
});
$(document).ready(function () {
    // Mảng lưu các thể loại đã chọn
    let selectedGenres = [];

    // Hàm tạo màu sắc ngẫu nhiên cho mỗi thể loại
    function generateRandomColor(id) {
        const hue = (id * 137) % 360;  // Tạo màu sắc ngẫu nhiên cho thể loại
        const saturation = 90 + (id % 10);
        const lightness = 30 + (id % 10);
        return `hsl(${hue}, ${saturation}%, ${lightness}%)`;
    }

    // Hàm cập nhật lại danh sách thể loại đã chọn trong giao diện
    function updateSelectedGenres() {
        const selectedContainer = $("#selected-genres");
        selectedContainer.empty();  // Xóa nội dung cũ

        selectedGenres.forEach(genre => {
            // Tạo thẻ <a> cho thể loại đã chọn
            const genreLink = $('<a>')
                .addClass("dropdown-item random-hover-link text-decoration-none unique-category-name unique-category-name-click")
                .attr("href", "/Home/IndexFilter?GenresSlected=" + genre.id)
                .attr("data-genre-id", genre.id)
                .attr("data-genre-name", genre.name)
                .text(genre.name);

            // Áp dụng màu sắc cho tên thể loại
            const color = generateRandomColor(genre.id);
            genreLink.css('color', color);

            // Tạo nút xóa thể loại
            const removeBtn = $('<button>')
                .addClass("btn-close ms-1")
                .css("font-size", "0.8rem")
                .on("click", function () {
                    event.preventDefault();
                    removeGenre(genre.id);  // Gọi hàm xóa thể loại
                    updateSelectedGenres();  // Cập nhật lại giao diện
                    updateHiddenInput();  // Cập nhật giá trị cho hidden input
                });

            // Thêm nút xóa vào trong thẻ <a>
            genreLink.append(removeBtn);

            // Thêm thẻ vào trong container
            selectedContainer.append(genreLink);
            selectedContainer.append(genreLink);
        });

        // Cập nhật giá trị cho hidden input
        updateHiddenInput();
    }

    // Hàm xử lý khi chọn thể loại
    function selectGenre(element, genreName) {
        const genreId = $(element).data("genre-id");

        // Kiểm tra nếu thể loại đã tồn tại trong danh sách thì không thêm nữa
        if (!selectedGenres.some(genre => genre.id === genreId)) {
            selectedGenres.push({ id: genreId, name: genreName });
            updateSelectedGenres();  // Cập nhật lại giao diện
        }
    }

    // Hàm cập nhật giá trị cho hidden input
    function updateHiddenInput() {
        const selectedGenresIds = selectedGenres.map(genre => genre.id).join(",");  // Tạo chuỗi ID
        $("#selectedGenresIds").val(selectedGenresIds);  // Cập nhật giá trị cho hidden input
    }

    // Hàm xóa thể loại khỏi danh sách
    function removeGenre(genreId) {
        const index = selectedGenres.findIndex(genre => genre.id === genreId);
        if (index !== -1) {
            selectedGenres.splice(index, 1);  // Xóa thể loại khỏi mảng
        }
    }

    // Khởi tạo mảng `selectedGenres` từ các thể loại đã chọn có sẵn trong giao diện
    // Lặp qua tất cả các thể loại đã chọn trong #selected-genres
    $("#selected-genres a").each(function () {
        // Lấy genreId và genreName từ data-attributes của thẻ <a>
        const genreId = $(this).data("genre-id");
        const genreName = $(this).data("genre-name");

        // Thêm vào mảng selectedGenres
        selectedGenres.push({ id: genreId, name: genreName });
    });

    // Lắng nghe sự kiện khi người dùng chọn thể loại từ danh sách
    $(".unique-category-name-click").on("click", function (event) {
        event.preventDefault();  // Ngừng hành động mặc định
        const genreName = $(this).data("genre-name");
        selectGenre(this, genreName);  // Gọi hàm xử lý chọn thể loại
    });

    // Nếu bạn có các phần tử thể loại đã có sẵn, có thể gọi updateSelectedGenres ở đây để hiển thị chúng
    updateSelectedGenres();
});

// Toggle the visibility of message box when the bell icon is clicked
$("#bellButton").click(function () {
    $("#messageBox").toggleClass("d-none");
});

// Handle sending message (you can replace with real functionality)
$("#sendMessageButton").click(function () {
    // Lấy giá trị của group (room) và thông điệp
    var roomName = document.getElementById("messageSelect").value;
    var message = document.getElementById("messageInput").value;

    // Kiểm tra nếu người dùng chọn group và nhập thông điệp
    if (roomName && message) {
        // Gửi thông điệp đến SignalR Hub
        connection.invoke("SendMessageToRoom", roomName, message)
            .then(function () {
                console.log("Message sent to room:", roomName);
            })
            .catch(function (err) {
                console.error("Error sending message:", err);
            });
    } else {
        alert("Vui lòng chọn nhóm và nhập thông báo.");
    }
});













