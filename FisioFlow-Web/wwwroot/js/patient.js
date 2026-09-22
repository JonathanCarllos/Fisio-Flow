document.addEventListener("DOMContentLoaded", function () {

    // ==========================
// ViaCEP
// ==========================

const cep = document.getElementById("PostalCode");


if (cep) {


    cep.addEventListener("blur", function () {


        let valor = this.value.replace(/\D/g, "");



        if (valor.length !== 8) {

            return;

        }



        fetch(`https://viacep.com.br/ws/${valor}/json/`)

            .then(response => response.json())

            .then(data => {


                if (data.erro) {


                    alert("CEP não encontrado.");

                    return;

                }



                document.getElementById("Address").value =
                    data.logradouro || "";


                document.getElementById("Neighborhood").value =
                    data.bairro || "";


                document.getElementById("City").value =
                    data.localidade || "";


                document.getElementById("State").value =
                    data.uf || "";


            })


            .catch(error => {

                console.error(
                    "Erro ao consultar CEP:",
                    error
                );

            });



    });



}


    // ==========================
    // Máscara CPF
    // 000.000.000-00
    // ==========================

    const cpf = document.getElementById("CPF");


    if (cpf) {

        cpf.addEventListener("input", function () {


            let valor = this.value.replace(/\D/g, "");


            valor = valor.substring(0, 11);



            valor = valor.replace(
                /(\d{3})(\d)/,
                "$1.$2"
            );


            valor = valor.replace(
                /(\d{3})(\d)/,
                "$1.$2"
            );


            valor = valor.replace(
                /(\d{3})(\d{1,2})$/,
                "$1-$2"
            );



            this.value = valor;


        });

    }





    // ==========================
    // Máscara Telefone
    // (11) 99999-9999
    // ==========================

    const phone = document.getElementById("Phone");


    if (phone) {


        phone.addEventListener("input", function () {


            let valor = this.value.replace(/\D/g, "");


            valor = valor.substring(0, 11);



            if (valor.length <= 10) {


                valor = valor.replace(
                    /(\d{2})(\d)/,
                    "($1) $2"
                );


                valor = valor.replace(
                    /(\d{4})(\d)/,
                    "$1-$2"
                );


            }
            else {


                valor = valor.replace(
                    /(\d{2})(\d)/,
                    "($1) $2"
                );


                valor = valor.replace(
                    /(\d{5})(\d)/,
                    "$1-$2"
                );


            }



            this.value = valor;


        });


    }






    // ==========================
    // Validação Email
    // ==========================

    const email = document.getElementById("Email");



    if (email) {


        email.addEventListener("blur", function () {



            const regex =
                /^[^\s@]+@[^\s@]+\.[^\s@]+$/;




            if (this.value !== "" &&
                !regex.test(this.value)) {


                this.classList.add("is-invalid");



            }
            else {


                this.classList.remove("is-invalid");


            }



        });


    }



});