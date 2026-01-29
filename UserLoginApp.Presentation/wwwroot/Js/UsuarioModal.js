let usModal = null;

$(function () {
    $(document).ready(function () {

        $("#btnNuevo").click(function () {
            console.log("Click detectado");

            $.get("/Usuario/Crear", function (data) {
                // Insert modal en el DOM
                $("#modalContainer").html(data);

                const modalElement = document.getElementById("usuarioModal");
                if (!modalElement) {
                    console.error("No se encontró el modal. Asegúrate que la acción GET retorna solo el PartialView");
                    return;
                }

                const usModal = bootstrap.Modal.getOrCreateInstance(modalElement);
                usModal.show();

                const form = modalElement.querySelector("form");
                if (!form) {
                    console.error("No se encontró el formulario dentro del modal");
                    return;
                }
            });
        });

        function manejarSubmit(e) {
            e.preventDefault();
            e.stopPropagation();

            const form = e.currentTarget;
            const formData = new FormData(form);

            fetch(form.action, {
                method: "POST",
                body: formData
            })
                .then(async response => {

                    if (response.redirected) {
                        console.error("REDIRECCIÓN DETECTADA:", response.url);
                        return;
                    }

                    const contentType = response.headers.get("content-type");

                    if (contentType && contentType.includes("application/json")) {
                        const result = await response.json();

                        if (result.success) {
                            sessionStorage.setItem("successMessage", result.message);
                            window.location.href = result.redirectUrl;
                        }
                    } else {
                        document.activeElement?.blur();

                        const html = await response.text();
                        const modal = document.getElementById("usuarioModal");

                        modal.querySelector("form").outerHTML = html;

                        const newForm = modal.querySelector("form");
                        newForm.addEventListener("submit", manejarSubmit);
                    }
                });
        }

    });

    // Editar usuario
        $(document).on("click", ".btnEditar", function () {
        var id = $(this).data("id");

        $.get("/Usuario/Editar", {id: id }, function (data) {
            $("#modalContainer").html(data);
        $("#usuarioModal").modal("show");
        });
    });
});



       
     