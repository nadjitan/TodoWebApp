// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function getISOLocal() {
    const date = new Date()
    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone
    return `${date.toLocaleDateString('en-CA', { timeZone })}` +
        `T${date.toLocaleTimeString('en', { timeZone, hour12: false })}`
}

const createdDateInput = document.getElementById("add-created-date")
if (createdDateInput) createdDateInput.setAttribute("value", getISOLocal())
