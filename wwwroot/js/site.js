// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function getISOLocal(date = new Date()) {
    const loc = Intl.DateTimeFormat().resolvedOptions().timeZone
    return `${date.toLocaleDateString('en-CA', { timeZone: loc })}` +
        `T${date.toLocaleTimeString('en', { timeZone: loc, hour12: false })}`
}

const createdDateInput = document.getElementById("add-created-date")

if (createdDateInput) {
    createdDateInput.setAttribute("value", getISOLocal())
}
