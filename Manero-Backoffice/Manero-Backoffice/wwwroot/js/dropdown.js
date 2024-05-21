window.onload = function () {
    let dropdownButton = document.querySelector('#btn-dropdown');
    let options = document.querySelectorAll('.dropdown-item');
    let selected = document.getElementById('selected');
    let dropdownMenu = document.querySelector('#dropdown-menu');



    dropdownButton.addEventListener('click', toggleMenu);
    options.forEach(option => {
        option.addEventListener('click', function () {
            selected.value = option.innerHTML;
            dropdownButton.innerHTML = selected.value + `<i class="fa-solid fa-caret-down"></i> <span>You selected ${selected.value}</span>`;

            selected.dispatchEvent(new Event('change'));

            toggleMenu();
        });
    });

    document.addEventListener('click', function (event) {
        if (!dropdownButton.contains(event.target) && !dropdownMenu.contains(event.target)) {
            dropdownMenu.style.display = 'none';
        }
    });


};

function toggleMenu() {
    let dropdownMenu = document.querySelector('#dropdown-menu');
    let display = dropdownMenu.style.display;
    dropdownMenu.style.display = display === 'none' ? 'block' : 'none';
}