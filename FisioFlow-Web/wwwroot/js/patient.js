document.addEventListener("DOMContentLoaded", function () {


    // =====================================================
    // ELEMENTOS
    // =====================================================

    const form = document.querySelector("form");

    const cep = document.getElementById("PostalCode");

    const cpf = document.getElementById("CPF");

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


                valor =
                    valor.replace(
                        /(\d{5})(\d)/,
                        "$1-$2"
                    );


            }


            this.value = valor;


        });



        // ViaCEP

        cep.addEventListener("blur", function () {


            let valor =
                this.value.replace(/\D/g, "");



            if (valor.length !== 8) {

                return;

            }



            fetch(
                `https://viacep.com.br/ws/${valor}/json/`
            )


                .then(response => response.json())


                .then(data => {



                    if (data.erro) {


                        alert(
                            "CEP não encontrado."
                        );


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





                    if (address)
                        address.value =
                            data.logradouro || "";



                    if (neighborhood)
                        neighborhood.value =
                            data.bairro || "";



                    if (city)
                        city.value =
                            data.localidade || "";



                    if (state)
                        state.value =
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
    // REMOVER MÁSCARA ANTES DO POST
    // =====================================================

    if (form) {


        form.addEventListener(
            "submit",
            function () {



                if (cpf) {

                    cpf.value =
                        cpf.value.replace(/\D/g, "");

                }




                if (cep) {

                    cep.value =
                        cep.value.replace(/\D/g, "");

                }




                if (phone) {

                    phone.value =
                        phone.value.replace(/\D/g, "");

                }



            }

        );


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



                }
                else {



                    this.classList.remove(
                        "is-invalid"
                    );



                }



            }

        );


    }



});