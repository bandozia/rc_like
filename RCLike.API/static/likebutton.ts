var mainElement: HTMLElement;

function likeButton(el: HTMLElement, token: string = null): void {

    if (token) {
        const xhr = new XMLHttpRequest();
        xhr.open('GET', "/api/like/validate-liker?token=" + token + "&url=" + window.location.href);
        xhr.onload = () => {
            if (xhr.status == 200) {
                let result = JSON.parse(xhr.responseText);
                if (result.isValid == true) {
                    if (result.hasLiked == false) {
                        el.appendChild(buildLikeButton(token));
                    } else {
                        el.appendChild(buildLikedDiv());
                    }
                }
            }
        }
        xhr.send();
    }    
    
    const count = buildLikeCount();
    el.appendChild(count);

    mainElement = el;
}

function buildLikeCount(): HTMLDivElement {
    const countSpan = document.createElement("div");
    countSpan.id = 'like-count';
    countSpan.style.float = 'left';

    const xhr = new XMLHttpRequest();
    xhr.open('GET', '/api/like/count?url=' + window.location.href);
    xhr.onload = () => {
        if (xhr.status == 200) {
            countSpan.innerHTML = xhr.responseText;
        }
    }
    xhr.send();

    return countSpan;
}

function buildLikeButton(token: string): HTMLDivElement {
    const likeDiv = document.createElement("div");
    likeDiv.id = "like-div";
    const likeBt = document.createElement("button");
    likeBt.innerText = "like!";

    likeBt.onclick = () => {
        likeBt.disabled = true;
        const xhr = new XMLHttpRequest();
        xhr.open('GET', '/api/like?url=' + window.location.href + '&token=' + token);
        xhr.onload = () => {
            if (xhr.status == 200)
                likeComplete();
        }
        xhr.send();
    };

    likeDiv.appendChild(likeBt);
    return likeDiv;
}

function buildLikedDiv(): HTMLDivElement {
    const likedDiv = document.createElement("div");
    likedDiv.id = "liked-div";
    likedDiv.style.float = 'left';

    const ico = document.createElement("img");
    ico.src = "heart.png";    
    likedDiv.appendChild(ico);        

    return likedDiv;
}

function likeComplete() {
    const likeCount = document.querySelector("#like-count");
    let count = parseInt(likeCount.textContent);
    let newCount = count > 0 ? count + 1 : 1;
    likeCount.textContent = newCount.toString();        
    mainElement.querySelector("#like-div").remove();
    mainElement.appendChild(buildLikedDiv());
    
}

