var mainElement;
function likeButton(el, token) {
    if (token === void 0) { token = null; }
    if (token) {
        var xhr_1 = new XMLHttpRequest();
        xhr_1.open('GET', "/api/like/validate-liker?token=" + token + "&url=" + window.location.href);
        xhr_1.onload = function () {
            if (xhr_1.status == 200) {
                var result = JSON.parse(xhr_1.responseText);
                if (result.isValid == true) {
                    if (result.hasLiked == false) {
                        el.appendChild(buildLikeButton(token));
                    }
                    else {
                        el.appendChild(buildLikedDiv());
                    }
                }
            }
        };
        xhr_1.send();
    }
    else {
        el.appendChild(buildLikedDiv());
    }
    var count = buildLikeCount();
    el.appendChild(count);
    mainElement = el;
}
function buildLikeCount() {
    var countSpan = document.createElement("div");
    countSpan.id = 'like-count';
    countSpan.style.float = 'left';
    var xhr = new XMLHttpRequest();
    xhr.open('GET', '/api/like/count?url=' + window.location.href);
    xhr.onload = function () {
        if (xhr.status == 200) {
            countSpan.innerHTML = xhr.responseText;
        }
        else if (xhr.status == 404) {
            countSpan.innerHTML = "0";
        }
    };
    xhr.send();
    return countSpan;
}
function buildLikeButton(token) {
    var likeDiv = document.createElement("div");
    likeDiv.id = "like-div";
    var likeBt = document.createElement("button");
    likeBt.innerText = "like!";
    likeBt.onclick = function () {
        likeBt.disabled = true;
        var xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/like?url=' + window.location.href + '&token=' + token);
        xhr.onload = function () {
            if (xhr.status == 200)
                likeComplete();
        };
        xhr.send();
    };
    likeDiv.appendChild(likeBt);
    return likeDiv;
}
function buildLikedDiv() {
    var likedDiv = document.createElement("div");
    likedDiv.id = "liked-div";
    likedDiv.style.float = 'left';
    var ico = document.createElement("img");
    ico.src = "heart.png";
    likedDiv.appendChild(ico);
    return likedDiv;
}
function likeComplete() {
    var likeCount = document.querySelector("#like-count");
    var count = parseInt(likeCount.textContent);
    var newCount = count > 0 ? count + 1 : 1;
    likeCount.textContent = newCount.toString();
    mainElement.querySelector("#like-div").remove();
    mainElement.appendChild(buildLikedDiv());
}
