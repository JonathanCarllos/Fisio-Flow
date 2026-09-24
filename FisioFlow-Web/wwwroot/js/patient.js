javascript
document.addEventListener("DOMContentLoaded", function () {


    // =====================================================
    // ELEMENTOS
    // =====================================================

    const form = document.querySelector("form");

    const cep = document.getElementById("PostalCode");

    const cpf = document.getElementById("CPF");

    const crefito = document.getElementById("Crefito");

    const phone = document.getElementById("Phone");

    const email = document.getElementById("Email");


    // =====================================================
    // MÁSCARA CEP
    // 00000-000
    // =====================================================

    if (cep) {

        cep.addEventListener("input", function () {

            let valor = this.value.replace(/\D/g, "");

            valor = valor.substring(0, 8);

            if (valor.length > 5) {

                valor = valor.replace(
                    /(\d{5})(\d)/,
                    "$1-$2"
                );

            }

            this.value = valor;

        });


        // =================================================
        // VIA CEP
        // =================================================

        cep.addEventListener("blur", function () {

            let valor = this.value.replace(/\D/g, "");

            if (valor.length !== 8) {
                return;
            }


            fetch(
                `https://viacep.com.br/ws/${valor}/json/`
            )

                .then(response => {

    if (!response.ok) {
        throw new Error("Erro na consulta do CEP.");
    }

    return response.json();

})

    .then(data => {

        if (data.erro) {

            alert("CEP não encontrado.");

            return;

        }


        const address =
            document.getElementById("Address");


        const neighborhood =
            document.getElementById("Neighborhood");


        const city =
            document.getElementById("City");


        const state =
            document.getElementById("State");


        if (address) {

            address.value =
                data.logradouro || "";

        }


        if (neighborhood) {

            neighborhood.value =
                data.bairro || "";

        }


        if (city) {

            city.value =
                data.localidade || "";

        }


        if (state) {

            state.value =
                data.uf || "";

        }

    })

    .catch(error => {

        console.error(
            "Erro ao consultar CEP:",
            error
        );

    });

        });

    }


// =====================================================
// MÁSCARA CPF
// 000.000.000-00
// =====================================================

if (cpf) {

    cpf.addEventListener("input", function () {

        let valor =
            this.value.replace(/\D/g, "");

        valor =
            valor.substring(0, 11);


        valor =
            valor.replace(
                /(\d{3})(\d)/,
                "$1.$2"
            );


        valor =
            valor.replace(
                /(\d{3})(\d)/,
                "$1.$2"
            );


        valor =
            valor.replace(
                /(\d{3})(\d{1,2})$/,
                "$1-$2"
            );


        this.value = valor;

    });


    // =================================================
    // VALIDAÇÃO CPF
    // =================================================

    cpf.addEventListener("blur", function () {

        const valor =
            this.value.replace(/\D/g, "");


        if (valor === "") {

            this.classList.remove("is-invalid");
            this.classList.remove("is-valid");

            return;

        }


        if (!validarCPF(valor)) {

            this.classList.add("is-invalid");
            this.classList.remove("is-valid");

        }
        else {

            this.classList.remove("is-invalid");
            this.classList.add("is-valid");

        }

    });

}


// =====================================================
// VALIDAÇÃO CPF
// =====================================================

function validarCPF(cpf) {

    if (!cpf || cpf.length !== 11) {
        return false;
    }


    // Impede CPFs como:
    // 11111111111
    // 22222222222
    // etc.

    if (/^(\d)\1{10}$/.test(cpf)) {
        return false;
    }


    let soma = 0;


    // Primeiro dígito

    for (let i = 0; i < 9; i++) {

        soma +=
            parseInt(cpf.charAt(i)) *
            (10 - i);

    }


    let resto =
        (soma * 10) % 11;


    if (resto === 10) {
        resto = 0;
    }


    if (
        resto !==
        parseInt(cpf.charAt(9))
    ) {

        return false;

    }


    // Segundo dígito

    soma = 0;


    for (let i = 0; i < 10; i++) {

        soma +=
            parseInt(cpf.charAt(i)) *
            (11 - i);

    }


    resto =
        (soma * 10) % 11;


    if (resto === 10) {
        resto = 0;
    }


    return (
        resto ===
        parseInt(cpf.charAt(10))
    );

}


// =====================================================
// CREFITO
// =====================================================
//
// Exemplos aceitos:
//
// CREFITO 3/123456-F
// CREFITO-3/123456-F
// 3/123456-F
// CREFITO 3/123456
// 3/123456
//
// =====================================================

if (crefito) {

    crefito.addEventListener("input", function () {

        let valor =
            this.value.toUpperCase();


        // Remove caracteres que não são
        // letras, números, barra ou hífen.

        valor =
            valor.replace(
                /[^A-Z0-9\/\-\s]/g,
                ""
            );


        // Normaliza espaços

        valor =
            valor.replace(
                /\s+/g,
                " "
            );


        this.value = valor;

    });


    crefito.addEventListener("blur", function () {

        const valor =
            this.value.trim().toUpperCase();


        if (valor === "") {

            this.classList.remove("is-invalid");
            this.classList.remove("is-valid");

            return;

        }


        if (!validarCREFITO(valor)) {

            this.classList.add("is-invalid");
            this.classList.remove("is-valid");

        }
        else {

            this.classList.remove("is-invalid");
            this.classList.add("is-valid");

        }

    });

}


// =====================================================
// VALIDAÇÃO CREFITO
// =====================================================

function validarCREFITO(valor) {

    if (!valor) {
        return false;
    }


    /*
     * Aceita:
     *
     * CREFITO 3/123456-F
     * CREFITO-3/123456-F
     * CREFITO 3/123456
     * CREFITO-3/123456
     * 3/123456-F
     * 3/123456
     *
     */


    const regex =
        /^(?:CREFITO[\s-]*)?(\d{1,2})\/(\d{3,8})(?:-([A-Z]))?$/i;


    const resultado =
        valor.match(regex);


    if (!resultado) {
        return false;
    }


    const regiao =
        parseInt(resultado[1]);


    const numero =
        resultado[2];


    /*
     * O número da região do CREFITO
     * deve estar entre 1 e 20.
     */

    if (
        regiao < 1 ||
        regiao > 20
    ) {

        return false;

    }


    /*
     * Registro precisa ter pelo menos
     * 3 números.
     */

    if (
        numero.length < 3 ||
        numero.length > 8
    ) {

        return false;

    }


    return true;

}


// =====================================================
// MÁSCARA TELEFONE
// (11) 99999-9999
// =====================================================

if (phone) {

    phone.addEventListener("input", function () {

        let valor =
            this.value.replace(/\D/g, "");


        valor =
            valor.substring(0, 11);


        if (valor.length <= 10) {

            valor =
                valor.replace(
                    /(\d{2})(\d)/,
                    "($1) $2"
                );


            valor =
                valor.replace(
                    /(\d{4})(\d)/,
                    "$1-$2"
                );

        }
        else {

            valor =
                valor.replace(
                    /(\d{2})(\d)/,
                    "($1) $2"
                );


            valor =
                valor.replace(
                    /(\d{5})(\d)/,
                    "$1-$2"
                );

        }


        this.value = valor;

    });

}


// =====================================================
// VALIDAÇÃO EMAIL
// =====================================================

if (email) {

    email.addEventListener(
        "blur",
        function () {

            const regex =
                /^[^\s@]+@[^\s@]+\.[^\s@]+$/;


            if (
                this.value !== "" &&
                !regex.test(this.value)
            ) {

                this.classList.add(
                    "is-invalid"
                );

                this.classList.remove(
                    "is-valid"
                );

            }
            else if (this.value !== "") {

                this.classList.remove(
                    "is-invalid"
                );

                this.classList.add(
                    "is-valid"
                );

            }
            else {

                this.classList.remove(
                    "is-invalid"
                );

                this.classList.remove(
                    "is-valid"
                );

            }

        }
    );

}


// =====================================================
// REMOVER MÁSCARAS ANTES DO POST
// =====================================================

if (form) {

    form.addEventListener(
        "submit",
        function () {


            // CPF

            if (cpf) {

                cpf.value =
                    cpf.value.replace(/\D/g, "");

            }


            // CEP

            if (cep) {

                cep.value =
                    cep.value.replace(/\D/g, "");

            }


            // TELEFONE

            if (phone) {

                phone.value =
                    phone.value.replace(/\D/g, "");

            }


            // CREFITO

            if (crefito) {

                crefito.value =
                    crefito.value
                        .trim()
                        .toUpperCase();

            }

        }
    );

}

});

