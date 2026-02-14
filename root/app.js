const api = "http://localhost:5115/";

async function loadUrls() {
    const responce = await fetch(api + "api/Url/GetUrls");
    const data = await responce.json();

    const tbody = document.getElementById("urlTableBody");
    tbody.innerHTML = "";

    data.forEach(item => {
        const tr = document.createElement("tr");

        tr.innerHTML = `
            <td>${item.longUrl}</td>
            <td><a href="${api + `U/` + item.shortUrl}" target="_blank">${item.shortUrl}</a></td>
            <td>${new Date(item.dateCreate).toLocaleString()}</td>
            <td>${item.countClick}</td>
            <td>
                <button class="action" onclick="editUrl(${item.id}, '${item.longUrl}')">Редактировать</button>
                <button class="action" onclick="deleteUrl(${item.id})">Удалить</button>
            </td>
        `;

        tbody.appendChild(tr);
    });
}

async function createShortUrl() {
    const longUrl = document.getElementById("longUrlInput").value.trim();
    if (!longUrl) return alert("Введите URL");

    const responce = await fetch(api + "api/Url/AddUrl?longUrl="+longUrl, {
        method: "POST"
    });

    if (!responce.ok) {
        alert("Ошибка при создании");
        return;
    }

    document.getElementById("longUrlInput").value = "";
    loadUrls();
}

async function deleteUrl(id) {
    if (!confirm("Удалить запись?")) return;

    await fetch(api + "api/Url/DeleteUrl?id=" + id, { method: "DELETE" });
    loadUrls();
}

async function editUrl(id, oldValue) {
    const newUrl = prompt("Введите новый URL", oldValue);
    if (!newUrl) return;

    await fetch(api + "api/Url/EditUrl?id=" + id + `&newLongUrl=` + newUrl, { method: "PUT" });

    loadUrls();
}

loadUrls();
