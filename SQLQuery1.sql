
-- Mostrare elenco dei prodotti con giacenza limitata (Quantità Disponibile < 10)

select * 
from Prodotto
where QuantitaDisponibile < 10

-- Mostrare il numero di prodotti per ogni categoria

select categoria as [Categoria Prodotti]
		, sum(QuantitaDisponibile) as [Totale Prodotti]	
from prodotto
group by categoria