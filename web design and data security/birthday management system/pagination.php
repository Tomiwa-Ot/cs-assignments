<nav>
    <ul class="pagination">
        <?php
            // Get base URL
            $baseUrl = strtok($_SERVER['REQUEST_URI'], '?');
            $getVariables = $_GET;

            // Remove page parameter if it exists
            unset($getVariables['page']);
            // Build query string
            $queryString = http_build_query($getVariables);

            $page = 1;
            if (isset($_GET['page']))
                $page = $_GET['page'];

            // Get total number of pages there can be
            $query = "SELECT COUNT(*) AS row_count  FROM STAFF";
            $result = mysqli_query($connection, $query);
            $numberOfPages = 0;
            if ($result) {
                $row = mysqli_fetch_assoc($result);
                $numberOfPages = ceil($row['row_count'] / 5);
            }

            // Define the number of links to show before and after the current page
            $numLinks = 1;
            
            // Previous page link
            if ($page > 1)
                echo '<a class="page-link" href="'. $baseUrl . '?page=' . ($page - 1) . '&' . $queryString . '">Previous</a>';
            
            // Page links
            for ($i = max(1, $page - $numLinks); $i <= min($page + $numLinks, $numberOfPages); $i++)
                echo '<a class="page-link" href="'. $baseUrl . '?page=' . $i . '&' . $queryString . '"' . ($i == $page ? ' class="active"' : '') . '>' . $i . '</a>';
            
            // Next page link
            if ($page < $numberOfPages)
                echo '<a class="page-link" href="'. $baseUrl . '?page=' . ($page + 1) . '&' . $queryString . '">Next</a>';
        ?>
    </ul>
</nav>
