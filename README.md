# PrototipoInterfacesInteligentes
1. Qué funciones se pueden usar en los scripts de Unity para llevar a cabo
traslaciones, rotaciones y escalados.
2. Como trasladarías la cámara 2 metros en cada uno de los ejes y luego la rotas 30º
alrededor del eje Y?. Rota la cámara alrededor del eje Y 30ª y desplázala 2 metros
en cada uno de los ejes. ¿Obtendrías el mismo resultado en ambos casos?. Justifica
el resultado
3. Sitúa la esfera de radio 1 en el campo de visión de la cámara y configura un volumen
de vista que la recorte parcialmente.
4. Sitúa la esfera de radio 1 en el campo de visión de la cámara y configura el volumen
de vista para que la deje fuera de la vista.
5. Como puedes aumentar el ángulo de la cámara. Qué efecto tiene disminuir el ángulo
de la cámara.
6. Es correcta la siguiente afirmación: Para realizar la proyección al espacio 2D, en el
inspector de la cámara, cambiaremos el valor de projection, asignándole el valor de
orthographic
7. Especifica las rotaciones que se han indicado en los ejercicios previos con la utilidad
quaternion.
8. ¿Como puedes averiguar la matriz de proyección en perspectiva que se ha usado
para proyectar la escena al último frame renderizado?.
9. ¿Como puedes averiguar la matriz de proyección en perspectiva ortográfica que se
ha usado para proyectar la escena al último frame renderizado?.
10. ¿Cómo puedes obtener la matriz de transformación entre el sistema de coordenadas
local y el mundial?.
11. Cómo puedes obtener la matriz para cambiar al sistema de referencia de vista
12. Especifica la matriz de la proyección usado en un instante de la ejecución del
ejercicio 1 de la práctica 1.
13. Especifica la matriz de modelo y vista de la escena del ejercicio 1 de la práctica 1.
14. Aplica una rotación en el start de uno de los objetos de la escena y muestra la matriz
de cambio al sistema de referencias mundial.
15. ¿Como puedes calcular las coordenadas del sistema de referencia de un objeto con
las siguientes propiedades del Transform:?:
Position (3, 1, 1), Rotation (45, 0, 45)
16. Crea una escena en Unity con los siguientes elementos: cámara principal, plano
base (como suelo) y tres cubos de distinto color (rojo, verde, azul) colocados en
posiciones distintas en el espacio. Realiza un pequeño script de depuración adjunto
a la cámara que permita visualizar en consola o en pantalla las matrices de
transformación (Model, View, Projection) y sus resultados sobre un vértice de cada
cubo.
17. Dibujar en un programa de dibujo el recorrido de las coordenadas de un vértice
específico del cubo rojo:
Local → World → Camera/View → Clip → NDC → Viewport. Indicar cómo cambia su
valor en cada espacio. Aplicar la transformación manualmente a un punto (por
ejemplo, el vértice (0.5, 0.5, 0.5)) y registrar los resultados paso a paso.
18. Mover o rotar uno de los cubos y mostrar cómo cambian los valores de su matriz de
modelo. Rotar la cámara y mostrar cómo se modifica la matriz de vista. Cambiar
entre proyección ortográfica y perspectiva y comparar las diferencias numéricas en
la matriz de proyección.
