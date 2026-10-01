document.addEventListener("DOMContentLoaded", function () {
    console.log("patient.js iniciado.");

    const form = document.querySelector("form");

    const cep = document.getElementById("PostalCode");
    const cpf = document.getElementById("CPF");
    const phone = document.getElementById("Phone");
    const email = document.getElementById("Email");

    const address = document.getElementById("Address");
    const neighborhood = document.getElementById("Neighborhood");
    const city = document.getElementById("City");
    const state = document.getElementById("State");


    // CEP 
    if (cep) {
        console.log("Campo PostalCode encontrado.");

        cep.addEventListener("input", function () {
            let valor = this.value.replace(/\D/g, "");

            valor = valor.substring(0, 8);

            if (valor.length > 5) {
                valor = valor.substring(0, 5) + "-" + valor.substring(5);
            }

            this.value = valor;
        });

        cep.addEventListener("blur", async function () {
            console.log("Evento BLUR do CEP executado.");

            const cepNumeros = this.value.replace(/\D/g, "");

            console.log("CEP informado:", cepNumeros);

            if (cepNumeros.length !== 8) {
                console.log("CEP inválido. Quantidade:", cepNumeros.length);

                return;
            }

            console.log("Iniciando consulta ao ViaCEP...");

            try {
                const url = "https://viacep.com.br/ws/" + cepNumeros + "/json/";

                console.log("URL:", url);

                const response = await fetch(url);

                console.log("HTTP:", response.status);

                if (!response.ok) {
                    throw new Error("Erro HTTP " + response.status);
                }

                const data = await response.json();

                console.log("Resposta do ViaCEP:", data);

                if (data.erro) {
                    alert("CEP não encontrado.");

                    return;
                }

              
                // PREENCHER ENDEREÇO             
                if (address) {
                    address.value = data.logradouro || "";
                }
              
                // PREENCHER BAIRRO
                if (neighborhood) {
                    neighborhood.value = data.bairro || "";
                }
            
                // PREENCHER CIDADE
                if (city) {
                    city.value = data.localidade || "";
                }
               
                // PREENCHER ESTADO
                if (state) {
                    state.value = data.uf || "";
                }

                console.log("Endereço preenchido.");
            } catch (error) {
                console.error("ERRO AO CONSULTAR VIACEP:", error);

                alert("Erro ao consultar o CEP. Veja o Console (F12).");
            }
        });
    }

    // CPF
    if (cpf) {
        cpf.addEventListener("input", function () {
            let valor = this.value.replace(/\D/g, "");

            valor = valor.substring(0, 11);

            if (valor.length > 9) {
                valor = valor.replace(
                    /^(\d{3})(\d{3})(\d{3})(\d{1,2})$/,
                    "$1.$2.$3-$4",
                );
            } else if (valor.length > 6) {
                valor = valor.replace(/^(\d{3})(\d{3})(\d{1,3})$/, "$1.$2.$3");
            } else if (valor.length > 3) {
                valor = valor.replace(/^(\d{3})(\d{1,3})$/, "$1.$2");
            }

            this.value = valor;
        });
    }

    // TELEFONE
    if (phone) {
        phone.addEventListener("input", function () {
            let valor = this.value.replace(/\D/g, "");

            valor = valor.substring(0, 11);

            if (valor.length > 10) {
                valor = valor.replace(/^(\d{2})(\d{5})(\d{1,4})$/, "($1) $2-$3");
            } else if (valor.length > 6) {
                valor = valor.replace(/^(\d{2})(\d{4})(\d{1,4})$/, "($1) $2-$3");
            } else if (valor.length > 2) {
                valor = valor.replace(/^(\d{2})(\d{1,5})$/, "($1) $2");
            }

            this.value = valor;
        });
    }
  
    // EMAIL
    if (email) {
        email.addEventListener("blur", function () {
            const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (this.value !== "" && !regex.test(this.value)) {
                this.classList.add("is-invalid");
                this.classList.remove("is-valid");
            } else if (this.value !== "") {
                this.classList.remove("is-invalid");
                this.classList.add("is-valid");
            } else {
                this.classList.remove("is-invalid");
                this.classList.remove("is-valid");
            }
        });
    }
 
    // REMOVER MÁSCARAS ANTES DO POST
    if (form) {
        form.addEventListener("submit", function () {
            if (cpf) {
                cpf.value = cpf.value.replace(/\D/g, "");
            }

            if (cep) {
                cep.value = cep.value.replace(/\D/g, "");
            }

            if (phone) {
                phone.value = phone.value.replace(/\D/g, "");
            }
        });
    }
});
